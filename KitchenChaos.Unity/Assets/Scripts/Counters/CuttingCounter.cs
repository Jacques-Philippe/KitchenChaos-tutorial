using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class CuttingCounter : BaseCounter, IHasProgress
    {
        /// <summary>
        /// A list of all items for which there is a slice recipe
        /// </summary>
        [SerializeField] private SliceRecipeSO[] mSliceRecipeSOs;

        /// <summary>
        /// Event fired 
        /// </summary>
        public event EventHandler OnCut;

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;


        /// <summary>
        /// The number of cuts performed so far. <br />
        /// For the number of cuts equal to the total number of cuts associated to the <see cref="SliceRecipeSO"/>, the input <see cref="KitchenObject"/> should be transformed into the <see cref="SliceRecipeSO"/> output.
        /// </summary>
        private int mCuts;

        ///// <inheritdoc/>
        //public override void Interact(Player player)
        //{
        //    KitchenObject counterKitchenObject = this.GetKitchenObject();
        //    KitchenObject playerKitchenObject = player.GetKitchenObject();
        //    //if there is no kitchen object on the counter and the player has a kitchen object
        //    if (counterKitchenObject == null && playerKitchenObject != null)
        //    {
        //        //if the kitchen object has a slice recipe
        //        if (HasASliceRecipe(playerKitchenObject))
        //        {
        //            //place the kitchen object on the cutting counter
        //            playerKitchenObject.setKitchenObjectParent(this);

        //            this.mCuts = 0;
        //            this.OnProgressChanged?.Invoke(sender: this, e: new IHasProgress.ProgressChangedEventArgs{ normalizedProgress = 0.0f });
        //        }
        //    }
        //    //else if there is a kitchen object on the counter and the player doesn't have a kitchen object, give it to the player
        //    else if (counterKitchenObject != null && playerKitchenObject == null)
        //    {
        //        counterKitchenObject.setKitchenObjectParent(player);

        //        this.mCuts = 0;
        //        this.OnProgressChanged?.Invoke(sender: this, e: new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 0.0f });
        //    }

        //}

        /// <inheritdoc/>
        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            //if the player has a kitchen object
            if (player.HasKitchenObject())
            {
                //if there is no kitchen object on the counter
                if (counterKitchenObject == null)
                {
                    //if the kitchen object has a slice recipe
                    if (HasASliceRecipe(playerKitchenObject))
                    {
                        //place the kitchen object on the cutting counter
                        playerKitchenObject.setKitchenObjectParent(this);

                        this.mCuts = 0;
                        this.OnProgressChanged?.Invoke(sender: this, e: new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 0.0f });
                    }
                }
                //else if there is a kitchen object on the counter
                else
                {
                    //if the player's kitchen object is a plate
                    if (playerKitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //try to add the counter kitchen object onto the plate
                        if (plateKitchenObject.TryAddIngredient(counterKitchenObject.GetKitchenObjectSO()))
                        {
                            counterKitchenObject.DestroySelf();
                        }

                    }
                }
            }
            //player doesn't have a kitchen object
            else
            {
                //if there is a kitchen object on the counter
                if (counterKitchenObject != null)
                {
                    //give it to the player
                    counterKitchenObject.setKitchenObjectParent(player);

                    this.mCuts = 0;
                    this.OnProgressChanged?.Invoke(sender: this, e: new IHasProgress.ProgressChangedEventArgs { normalizedProgress = 0.0f });
                }
            }
            

        }

        /// <inheritdoc/>
        public override void AlternateInteract(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            //if there is a kitchen object on the counter
            if (counterKitchenObject != null)
            {
                //given the associated slice recipe...
                if (this.TryGetSliceRecipeForKitchenObjectInput(inputKitchenObjectSO: counterKitchenObject.GetKitchenObjectSO(), out SliceRecipeSO sliceRecipe))
                {
                    this.mCuts++;

                    float percentage = (float)this.mCuts / sliceRecipe.maxCuts;
                    
                    this.OnProgressChanged?.Invoke(sender: this, e: new IHasProgress.ProgressChangedEventArgs { normalizedProgress = percentage });
                    this.OnCut?.Invoke(sender: this, e: EventArgs.Empty);

                    if (this.mCuts >= sliceRecipe.maxCuts)
                    {
                        counterKitchenObject.DestroySelf();
                        //slice it
                        KitchenObject.SpawnKitchenObject(sliceRecipe.output, this);
                    }
                }
            }
        }

        /// <summary>
        /// REturn true for the provided <paramref name="kitchenObject"/> has a slice recipe in <see cref="mSliceRecipeSOs"/>
        /// </summary>
        /// <param name="kitchenObject"></param>
        /// <returns></returns>
        private bool HasASliceRecipe(KitchenObject kitchenObject)
        {
            return this.TryGetSliceRecipeForKitchenObjectInput(inputKitchenObjectSO: kitchenObject.GetKitchenObjectSO(), out SliceRecipeSO sliceRecipe);
        }

        /// <summary>
        /// Given our <see cref="mSliceRecipeSOs"/>, return the output <see cref="KitchenObjectSO"/> for the given input <see cref="KitchenObjectSO"/>
        /// </summary>
        /// <param name="inputKitchenObjectSO"></param>
        /// <returns></returns>
        private bool TryGetSliceRecipeForKitchenObjectInput(KitchenObjectSO inputKitchenObjectSO, out SliceRecipeSO sliceRecipe)
        {
            sliceRecipe = null;
            foreach (var recipe in this.mSliceRecipeSOs)
            {
                if (recipe.input == inputKitchenObjectSO)
                {
                    sliceRecipe = recipe;
                    return true;
                }
            }
            return false;
        }
    }

}