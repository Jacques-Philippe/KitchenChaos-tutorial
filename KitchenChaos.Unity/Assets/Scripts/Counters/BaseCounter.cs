using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform mCounterTopPoint;


        /// <summary>
        /// A reference to the kitchen object associated to the counter
        /// </summary>
        private KitchenObject mKitchenObject;

        /// <summary>
        /// Event fired for a kitchen object placed onto a counter
        /// </summary>
        public static event EventHandler OnSomethingPutDown;

        /// <summary>
        /// Function called to clear all subscribers of static event <see cref="OnSomethingPutDown"/>
        /// </summary>
        public static void ResetStaticData()
        {
            OnSomethingPutDown = null;
        }

        /// <summary>
        /// A function to determine how the counter will respond to player interaction
        /// </summary>
        /// <param name="player"></param>
        public abstract void Interact(Player player);

        /// <summary>
        /// An alternate interaction function to be defined, where appropriate (it's not obligated)
        /// </summary>
        /// <param name="player"></param>
        public virtual void AlternateInteract(Player player)
        {
            //do something
        }


        /// <inheritdoc/>
        public void ClearKitchenObject()
        {
            this.mKitchenObject = null;
        }

        /// <inheritdoc/>
        public KitchenObject GetKitchenObject()
        {
            return this.mKitchenObject;
        }

        /// <inheritdoc/>
        public Transform GetKitchenObjectFollowTransform()
        {
            return this.mCounterTopPoint;
        }

        /// <inheritdoc/>
        public bool HasKitchenObject()
        {
            return this.mKitchenObject != null;
        }

        /// <inheritdoc/>
        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            this.mKitchenObject = newKitchenObject;

            BaseCounter.OnSomethingPutDown?.Invoke(sender: this, e: EventArgs.Empty);
        }
    }
}
