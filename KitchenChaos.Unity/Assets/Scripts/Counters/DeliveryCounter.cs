using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            KitchenObject playerKitchenObject = player.GetKitchenObject();

            //if the player has a kitchen object
            if (playerKitchenObject != null)
            {
                //if the kitchen object is a plate
                if (playerKitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    DeliveryManager.Instance.Deliver(plateKitchenObject.GetKitchenObjectSOList());
                    //then accept the plate
                    plateKitchenObject.DestroySelf();
                }
            }
        }

    }

}