using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
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

        /// <inheritdoc/>
        public Transform GetKitchenObjectFollowTransform()
        {
            return this.mCounterTopPoint;
        }

        /// <inheritdoc/>
        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            this.mKitchenObject = newKitchenObject;
        }

        /// <inheritdoc/>
        public KitchenObject GetKitchenObject()
        {
            return this.mKitchenObject;
        }

        /// <inheritdoc/>
        public void ClearKitchenObject()
        {
            this.mKitchenObject = null;
        }

        /// <inheritdoc/>
        public bool HasKitchenObject()
        {
            return this.mKitchenObject != null;
        }


    }

}