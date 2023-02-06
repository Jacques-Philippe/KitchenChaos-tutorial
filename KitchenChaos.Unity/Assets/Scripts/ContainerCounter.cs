using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ContainerCounter : BaseCounter
    {
        /// <summary>
        /// A reference to the <see cref="KitchenObjectSO"/> object which should drive this table's functionality
        /// </summary>
        [SerializeField] private KitchenObjectSO mKitchenObjectSO;

        public event EventHandler ContainerOpened;

        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            //if there is no kitchen object on the counter and the player has no kitchen object, spawn it
            if (counterKitchenObject == null && playerKitchenObject == null)
            {
                GameObject kitchenGameObj = Instantiate(original: mKitchenObjectSO.Prefab);
                if (kitchenGameObj.TryGetComponent<KitchenObject>(out KitchenObject _kitchenObject))
                {
                    _kitchenObject.setKitchenObjectParent(player);
                    this.ContainerOpened?.Invoke(sender: this, e: EventArgs.Empty);
                }
            }
            else if (counterKitchenObject == null && playerKitchenObject != null)
            {
                playerKitchenObject.setKitchenObjectParent(this);
            }
            //else if there is a kitchen object on the counter and the player has no kitchen object, give it to the player
            else if (counterKitchenObject != null && playerKitchenObject == null)
            {
                counterKitchenObject.setKitchenObjectParent(player);
            }

        }
    }

}