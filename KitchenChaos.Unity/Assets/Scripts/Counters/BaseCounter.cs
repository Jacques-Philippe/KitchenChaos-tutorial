using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform mCounterTopPoint;

        private KitchenObject mKitchenObject;

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
        }
    }
}
