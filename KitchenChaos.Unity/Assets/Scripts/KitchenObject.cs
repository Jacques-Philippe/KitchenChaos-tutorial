using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO mKitchenObjectSO;

        /// <summary>
        /// The counter to which the kitchen object is associated to
        /// </summary>
        private IKitchenObjectParent mKitchenObjectParent;

        /// <summary>
        /// A function to update a Kitchen object's <see cref="mKitchenObjectParent"/>, where changing the counter also changes this kitchen object's position with respect to the clear counter's counter top point.
        /// </summary>
        /// <param name="newParent"></param>
        public void setKitchenObjectParent(IKitchenObjectParent newParent)
        {
            //Update the new counter

            //if there exists already a counter
            if (this.mKitchenObjectParent != null)
            {
                //Remove the kitchen object from that counter
                this.mKitchenObjectParent.ClearKitchenObject();
                //Remove the reference to the old counter
                this.mKitchenObjectParent = null;
            }
            //Update the counter associated to this kitchen object
            this.mKitchenObjectParent = newParent;
            //Update the kitchen object associated to the counter
            this.mKitchenObjectParent.SetKitchenObject(this);
            //Update the position of this kitchen object
            this.transform.parent = this.mKitchenObjectParent.GetKitchenObjectFollowTransform();
            this.transform.localPosition = Vector3.zero;
        }

        public void DestroySelf()
        {
            this.mKitchenObjectParent.ClearKitchenObject();
            GameObject.Destroy(this.gameObject);
        }
    }
}
