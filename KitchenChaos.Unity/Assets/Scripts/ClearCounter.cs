using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ClearCounter : MonoBehaviour
    {
        /// <summary>
        /// A reference to the <see cref="KitchenObjectSO"/> object which should drive this table's functionality
        /// </summary>
        [SerializeField] private KitchenObjectSO mKitchenObjectSO;
        /// <summary>
        /// A reference to a point located on the counter top
        /// </summary>
        [SerializeField] private Transform mCounterTopPoint;

        /// <summary>
        /// A reference to the kitchen object currently on the counter, if any
        /// </summary>
        private KitchenObject mKitchenObject;

        public void Interact()
        {
            Debug.Log("Interact!");
            GameObject kitchenGameObj = Instantiate(original: mKitchenObjectSO.Prefab, position: mCounterTopPoint.position, rotation: Quaternion.identity, parent: this.transform);
            if (kitchenGameObj.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject))
            {
                this.mKitchenObject = kitchenObject;
                kitchenObject.SetClearCounter(this);
            }

        }

        /// <summary>
        /// A function to set the kitchen object on this counter to the provided <paramref name="newKitchenObject"/>
        /// </summary>
        /// <param name="newKitchenObject"></param>
        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            this.mKitchenObject = newKitchenObject;
        }

        /// <summary>
        /// A function to get the kitchen object on this counter
        /// </summary>
        /// <param name="newKitchenObject"></param>
        public KitchenObject GetKitchenObject()
        {
            return this.mKitchenObject;
        }

        public void ClearKitchenObjectFromCounter()
        {
            this.mKitchenObject = null;
        }

        /// <summary>
        /// Whether there is currently a kitchen object on this counter
        /// </summary>
        /// <returns></returns>
        public bool HasKitchenObject()
        {
            return this.mKitchenObject != null;
        }


    }

}