using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class CuttingCounter : BaseCounter
    {
        /// <summary>
        /// A list of all items for which there is a slice recipe
        /// </summary>
        [SerializeField] private SliceRecipeSO[] mSliceRecipeSOs;

        /// <summary>
        /// The number of cuts performed so far. <br />
        /// For the number of cuts equal to the total number of cuts associated to the <see cref="SliceRecipeSO"/>, the input <see cref="KitchenObject"/> should be transformed into the <see cref="SliceRecipeSO"/> output.
        /// </summary>
        private int mCuts;

        /// <inheritdoc/>
        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            //if there is no kitchen object on the counter and the player has a kitchen object
            if (counterKitchenObject == null && playerKitchenObject != null)
            {
                //if the kitchen object has a slice recipe
                if (HasASliceRecipe(playerKitchenObject))
                {
                    //place the kitchen object on the cutting counter
                    playerKitchenObject.setKitchenObjectParent(this);
                    this.mCuts = 0;
                }
            }
            //else if there is a kitchen object on the counter and the player doesn't have a kitchen object, give it to the player
            else if (counterKitchenObject != null && playerKitchenObject == null)
            {
                counterKitchenObject.setKitchenObjectParent(player);
            }
        }

        /// <inheritdoc/>
        public override void AlternateInteract(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            //if there is a kitchen object on the counter
            if (counterKitchenObject != null)
            {
                this.mCuts++;
               
                SliceRecipeSO recipe = this.GetSliceRecipeForKitchenObjectInput(inputKitchenObjectSO: counterKitchenObject.GetKitchenObjectSO());

                if (recipe != null && this.mCuts >= recipe.maxCuts)
                {
                    counterKitchenObject.DestroySelf();
                    //slice it
                    GameObject slicedGameObject = Instantiate(original: recipe.output.Prefab);
                    if (slicedGameObject.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject))
                    {
                        kitchenObject.setKitchenObjectParent(this);
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
            SliceRecipeSO recipe = this.GetSliceRecipeForKitchenObjectInput(inputKitchenObjectSO: kitchenObject.GetKitchenObjectSO());
            return recipe != null;
        }

        /// <summary>
        /// Given our <see cref="mSliceRecipeSOs"/>, return the output <see cref="KitchenObjectSO"/> for the given input <see cref="KitchenObjectSO"/>
        /// </summary>
        /// <param name="inputKitchenObjectSO"></param>
        /// <returns></returns>
        private SliceRecipeSO GetSliceRecipeForKitchenObjectInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach(var sliceRecipe in this.mSliceRecipeSOs)
            {
                if (sliceRecipe.input == inputKitchenObjectSO)
                {
                    return sliceRecipe;
                }
            }
            return null;
        }
    }

}