using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public interface IKitchenObjectParent
    {
        /// <summary>
        /// A function to return the counter's top point transform
        /// </summary>
        /// <returns></returns>
        public Transform GetKitchenObjectFollowTransform();

        /// <summary>
        /// A function to set the kitchen object on this instance to the provided <paramref name="newKitchenObject"/>
        /// </summary>
        /// <param name="newKitchenObject"></param>
        public void SetKitchenObject(KitchenObject newKitchenObject);

        /// <summary>
        /// A function to get the kitchen object associated to this instance
        /// </summary>
        /// <param name="newKitchenObject"></param>
        public KitchenObject GetKitchenObject();

        /// <summary>
        /// A function to remove this instance's reference to the kitchen object
        /// </summary>
        public void ClearKitchenObject();

        /// <summary>
        /// Whether there is currently a kitchen object on this instance
        /// </summary>
        /// <returns></returns>
        public bool HasKitchenObject();
    }

}