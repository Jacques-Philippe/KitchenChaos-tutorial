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

        //TODO DELETE ME
        [SerializeField] private bool mTesting;

        [SerializeField] private ClearCounter mSecondCounter;

        /// <summary>
        /// A reference to the kitchen object currently on the counter, if any
        /// </summary>
        private KitchenObject mKitchenObject;

        private void Update()
        {
            if (this.mTesting && Input.GetKeyDown(KeyCode.T))
            {
                //Given the kitchen game object is spawned and associated to this counter
                //Transfer it to another counter
                this.mKitchenObject.SetClearCounter(mSecondCounter);
            }
        }

        public void Interact()
        {
            Debug.Log("Interact!");
            if (this.mKitchenObject == null)
            {
                GameObject kitchenGameObj = Instantiate(original: mKitchenObjectSO.Prefab);
                if (kitchenGameObj.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject))
                {
                    kitchenObject.SetClearCounter(this);
                }
            }

        }

        /// <summary>
        /// A function to return the counter's top point transform
        /// </summary>
        /// <returns></returns>
        public Transform GetCounterTopPoint()
        {
            return this.mCounterTopPoint;
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