using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class TrashCounter : BaseCounter
    {
        /// <summary>
        /// Event fired for an object thrown away
        /// </summary>
        public static event EventHandler OnAnyKitchenObjectTrashed;

        /// <summary>
        /// Function called to clear all subscribers of static event <see cref="OnAnyKitchenObjectTrashed"/>
        /// </summary>
        public static new void ResetStaticData()
        {
            OnAnyKitchenObjectTrashed = null;
        }

        /// <inheritdoc/>
        public override void Interact(Player player)
        {
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            //if the player has a kitchen object,
            if (playerKitchenObject != null)
            {
                //destroy it
                playerKitchenObject.DestroySelf();
                OnAnyKitchenObjectTrashed?.Invoke(sender: this, e: EventArgs.Empty);
            }
        }
    }
}
