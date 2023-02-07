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
                }
            }
            //else if there is a kitchen object on the counter and the player doesn't have a kitchen object, give it to the player
            else if (counterKitchenObject != null && playerKitchenObject == null)
            {
                counterKitchenObject.setKitchenObjectParent(player);
            }
        }

        public override void AlternateInteract(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            //if there is a kitchen object on the counter
            if (counterKitchenObject != null)
            {
                KitchenObjectSO output = this.GetKitchenObjectOutputForInput(inputKitchenObjectSO: counterKitchenObject.GetKitchenObjectSO());
                if (output != null)
                {
                    counterKitchenObject.DestroySelf();
                    //slice it
                    GameObject slicedGameObject = Instantiate(original: output.Prefab);
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
            KitchenObjectSO output = this.GetKitchenObjectOutputForInput(inputKitchenObjectSO: kitchenObject.GetKitchenObjectSO());
            return output != null;
        }

        /// <summary>
        /// Given our <see cref="mSliceRecipeSOs"/>, return the output <see cref="KitchenObjectSO"/> for the given input <see cref="KitchenObjectSO"/>
        /// </summary>
        /// <param name="inputKitchenObjectSO"></param>
        /// <returns></returns>
        private KitchenObjectSO GetKitchenObjectOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach(var sliceRecipe in this.mSliceRecipeSOs)
            {
                if (sliceRecipe.input == inputKitchenObjectSO)
                {
                    return sliceRecipe.output;
                }
            }
            return null;
        }
    }

}