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
            //if there is no kitchen object on the counter and the player has a kitchen object
            if (counterKitchenObject == null && playerKitchenObject != null)
            {
                playerKitchenObject.setKitchenObjectParent(this);
            }
            //else if there is a kitchen object on the counter and the player doesn't have a kitchen object, give it to the player
            else if (counterKitchenObject != null && playerKitchenObject == null)
            {
                counterKitchenObject.setKitchenObjectParent(player);
            }

        }



    }

}