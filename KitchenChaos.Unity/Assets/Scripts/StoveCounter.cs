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

        public enum State
        {
            Idle,
            Frying,
            Burning,
            Burned
        };

        public State state { private set; get; }

        private float burnTimer;
        private BurnRecipeSO burnRecipeSO;

        private float fryTimer;
        private FryRecipeSO fryRecipeSO;

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

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
            //if the player has an item which can be fried
            if (playerKitchenObject != null && this.HasFryRecipe(playerKitchenObject))
            {
                this.fryRecipeSO = GetFryRecipeSO(playerKitchenObject);

                //place the item on the stove
                playerKitchenObject.setKitchenObjectParent(this);

                //Start frying
                this.fryTimer = 0.0f;
                this.burnTimer = 0.0f;
                this.state = State.Frying;
            }
            //else if the player has no item and there is an item on the stove
            else if (playerKitchenObject == null && counterKitchenObject != null)
            {
                //pick up the kitchen object and give it to the player
                counterKitchenObject.setKitchenObjectParent(player);
                this.OnProgressChanged?.Invoke(sender: this, new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 0.0f });

                this.state = State.Idle;
            }
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
