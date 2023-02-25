using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    public class StoveCounter : BaseCounter, IHasProgress
    {

        [SerializeField] private FryRecipeSO[] fryRecipes;
        [SerializeField] private BurnRecipeSO[] burnRecipes;

        /// <summary>
        /// States to define stove behaviour. <br />
        /// The stove is <see cref="Idle"/> for no kitchen object on the stove. <br />
        /// The stove is <see cref="Frying"/> for a kitchen object with a Fry Recipe put onto the stove. <br />
        /// The stove is <see cref="Burning"/> for a kitchen object has fried and now is starting to burn. Note this kitchen object must have a Burn Recipe <br />
        /// The stove is <see cref="Burned"/> for the kitchen object has completed burning
        /// </summary>
        public enum State
        {
            /// <summary>
            /// The state for nothing on the stove
            /// </summary>
            Idle,
            /// <summary>
            /// The state for something added to the stove and cooking, but not yet cooked
            /// </summary>
            Frying,
            /// <summary>
            /// The state for something cooked, and starting to burn
            /// </summary>
            Burning,
            /// <summary>
            /// The state for something burned
            /// </summary>
            Burned
        };

        /// <summary>
        /// The stove's current state
        /// </summary>
        public State state { private set; get; }

        /// <summary>
        /// the timer to keep track of the burn timer progress
        /// </summary>
        private float burnTimer;
        /// <summary>
        /// The currently active burn recipe SO
        /// </summary>
        private BurnRecipeSO burnRecipeSO;

        /// <summary>
        /// the timer to keep track of the fry timer progress
        /// </summary>
        private float fryTimer;
        /// <summary>
        /// the currently active fry recipe SO
        /// </summary>
        private FryRecipeSO fryRecipeSO;

        /// <inheritdoc/>
        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        /// <summary>
        /// Event fired for the stove turned on
        /// </summary>
        public event EventHandler OnStoveOn;
        /// <summary>
        /// Event fired for the food on the stove has burned
        /// </summary>
        public event EventHandler OnFoodBurned;
        /// <summary>
        /// Event fired for the food has been removed from the stove
        /// </summary>
        public event EventHandler OnFoodRemoved;

        private void Start()
        {
            state = State.Idle;
        }

        private void Update()
        {

            switch (state)
            {
                case State.Idle:
                    {
                        break;
                    }
                case State.Frying:
                    {
                        this.fryTimer += Time.deltaTime;

                        float normalizedProgress = this.fryTimer / this.fryRecipeSO.TimeToFry;    
                        this.OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { normalizedProgress = normalizedProgress });

                        if (fryTimer >= this.fryRecipeSO.TimeToFry)
                        {
                            Fry();
                            this.fryTimer = 0.0f;
                            this.fryRecipeSO = null;
                            this.state = State.Burning;

                            this.burnRecipeSO = this.GetBurnRecipeSO(this.GetKitchenObject());
                        }
                        break;
                    }
                case State.Burning:
                    {
                        this.burnTimer += Time.deltaTime;

                        float normalizedProgress = this.burnTimer / this.burnRecipeSO.TimeToBurn;
                        this.OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { normalizedProgress = normalizedProgress });

                        if (burnTimer >= this.burnRecipeSO.TimeToBurn)
                        {
                            Burn();

                            this.burnTimer = 0.0f;
                            this.burnRecipeSO = null;
                            this.state = State.Burned;
                            this.OnFoodBurned?.Invoke(this, EventArgs.Empty);
                            this.OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 1 });
                        }
                        break;
                    }
                case State.Burned:
                    {
                        break;
                    }
            }
        }


        public override void Interact(Player player)
        {
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            //if the player has a kitchen object
            if (player.HasKitchenObject())
            {
                //if there is a kitchen object on the counter
                if (counterKitchenObject != null)
                {
                    //if the player has a plate
                    if (playerKitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //try to add the counter kitchen object to the plate
                        if (plateKitchenObject.TryAddIngredient(counterKitchenObject.GetKitchenObjectSO()))
                        {
                            counterKitchenObject.DestroySelf();

                            this.ResetStateForFoodRemoved();
                        }
                    }
                }
                //else if there is no kitchen object on the counter
                else
                {
                    //if the player has an item which can be fried
                    if (this.HasFryRecipe(playerKitchenObject))
                    {
                        this.fryRecipeSO = GetFryRecipeSO(playerKitchenObject);

                        //place the item on the stove
                        playerKitchenObject.setKitchenObjectParent(this);

                        //Start frying
                        this.fryTimer = 0.0f;
                        this.burnTimer = 0.0f;
                        this.state = State.Frying;
                        this.OnStoveOn?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            //else if the player does not have a kitchen object
            else
            {
                //if there is a kitchen object on the counter
                if (counterKitchenObject != null)
                {
                    //pick up the kitchen object and give it to the player
                    counterKitchenObject.setKitchenObjectParent(player);

                    this.ResetStateForFoodRemoved();
                }
            }

        }

        /// <summary>
        /// Whether the stove is currently burning an item
        /// </summary>
        /// <returns></returns>
        public bool IsBurning()
        {
            return this.state == State.Burning;
        }

        private void ResetStateForFoodRemoved()
        {
            this.OnProgressChanged?.Invoke(sender: this, new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 0.0f });

            this.state = State.Idle;
            this.OnFoodRemoved?.Invoke(this, EventArgs.Empty);
        }


        /// <summary>
        /// Whether the <paramref name="inputKitchenObject"/> can be fried
        /// </summary>
        /// <param name="inputKitchenObject"></param>
        private bool HasFryRecipe(KitchenObject inputKitchenObject)
        {
            foreach (var recipe in fryRecipes)
            {
                if (inputKitchenObject.GetKitchenObjectSO() == recipe.inputKitchenObject)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Helper to return a Fry Recipe SO given <paramref name="inputKitchenObject"/>
        /// </summary>
        /// <param name="inputKitchenObject"></param>
        /// <returns></returns>
        private FryRecipeSO GetFryRecipeSO(KitchenObject inputKitchenObject)
        {
            foreach (var recipe in fryRecipes)
            {
                if (inputKitchenObject.GetKitchenObjectSO() == recipe.inputKitchenObject)
                {
                    return recipe;
                }
            }
            return null;
        }

        /// <summary>
        /// Helper to return a Burn Recipe SO given <paramref name="inputKitchenObject"/>
        /// </summary>
        /// <param name="inputKitchenObject"></param>
        /// <returns></returns>
        private BurnRecipeSO GetBurnRecipeSO(KitchenObject inputKitchenObject)
        {
            foreach (var recipe in burnRecipes)
            {
                if (inputKitchenObject.GetKitchenObjectSO() == recipe.inputKitchenObject)
                {
                    return recipe;
                }
            }
            return null;
        }

        /// <summary>
        /// helper to fry the kitchen object currently being fried
        /// </summary>
        private void Fry()
        {
            //Get reference to next counter kitchen object to spawn
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            FryRecipeSO fryRecipe = this.GetFryRecipeSO(counterKitchenObject);

            //Destroy existing counter kitchen object
            counterKitchenObject.DestroySelf();
            //Spawn new counter kitchen object
            KitchenObject.SpawnKitchenObject(fryRecipe.outputKitchenObject, this);
        }

        /// <summary>
        /// Helper to burn the kitchen object currently being burned
        /// </summary>
        private void Burn()
        {
            //Get reference to next counter kitchen object to spawn
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            BurnRecipeSO burnRecipe = this.GetBurnRecipeSO(counterKitchenObject);

            //Destroy existing counter kitchen object
            counterKitchenObject.DestroySelf();
            //Spawn new counter kitchen object
            KitchenObject.SpawnKitchenObject(burnRecipe.outputKitchenObject, this);
        }
    }

}
