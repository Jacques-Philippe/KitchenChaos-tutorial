using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            //if the player has a kitchen object,
            if (playerKitchenObject != null)
            {
                //destroy it
                playerKitchenObject.DestroySelf();
            }
        }
    }
}
