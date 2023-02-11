using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ClearCounter : BaseCounter
    {

        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();

            //if the player is holding a KitchenObject
            if (player.HasKitchenObject())
            {

                //if there is no kitchen object on the counter
                if (counterKitchenObject == null)
                {
                    //put the player's kitchen object onto the counter
                    playerKitchenObject.setKitchenObjectParent(this);
                }
                //if there is a kitchen object on the counter
                else
                {
                    //if the player's kitchen object is a plate
                    if (playerKitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //try to add the counter kitchen object onto the player plate
                        if (plateKitchenObject.TryAddIngredient(counterKitchenObject.GetKitchenObjectSO()))
                        {
                            counterKitchenObject.DestroySelf();
                        }
                    }
                    //else if the counter's kitchen object is a plate
                    else if (counterKitchenObject.TryGetPlate(out plateKitchenObject))
                    {
                        //try to add the player kitchen object onto the counter plate
                        if (plateKitchenObject.TryAddIngredient(playerKitchenObject.GetKitchenObjectSO()))
                        {
                            playerKitchenObject.DestroySelf();
                        }
                    }
                }
            }
            //else if the player is not holding a KitchenObject
            else {
                //if there is a kitchen object on the counter
                if (counterKitchenObject != null)
                {
                    //give the player the KitchenObject
                    counterKitchenObject.setKitchenObjectParent(player);
                }
            }


        }



    }

}