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
        private ClearCounter mClearCounter;

        /// <summary>
        /// A function to update a Kitchen object's <see cref="mClearCounter"/>, where changing the counter also changes this kitchen object's position with respect to the clear counter's counter top point.
        /// </summary>
        /// <param name="newClearCounter"></param>
        public void SetClearCounter(ClearCounter newClearCounter)
        {
            //Update the new counter

            //if there exists already a counter
            if (this.mClearCounter != null)
            {
                //Remove the kitchen object from that counter
                this.mClearCounter.ClearKitchenObjectFromCounter();
                //Remove the reference to the old counter
                this.mClearCounter = null;
            }
            //Update the counter associated to this kitchen object
            this.mClearCounter = newClearCounter;
            //Update the kitchen object associated to the counter
            this.mClearCounter.SetKitchenObject(this);
            //Update the position of this kitchen object
            this.transform.parent = this.mClearCounter.GetCounterTopPoint();
            this.transform.localPosition = Vector3.zero;
            
        }
    }
}
