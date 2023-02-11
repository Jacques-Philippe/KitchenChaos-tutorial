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

        /// <summary>
        /// Function to clear this kitchen object's parent, and then destroy this gameobject instance
        /// </summary>
        public void DestroySelf()
        {
            this.mKitchenObjectParent.ClearKitchenObject();
            GameObject.Destroy(this.gameObject);
        }

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return this.mKitchenObjectSO;
        }

        /// <summary>
        /// Helper function to get the instance of <see cref="PlateKitchenObject"/> associated to this <see cref="KitchenObject"/>, if any
        /// </summary>
        /// <param name="plateKitchenObject"></param>
        /// <returns></returns>
        public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
        {
            if (this is PlateKitchenObject)
            {
                plateKitchenObject = this as PlateKitchenObject;
                return true;
            }
            else
            {
                plateKitchenObject = null;
                return false;
            }
        }

        /// <summary>
        /// Static function to spawn prefab of <paramref name="kitchenObjectSO"/>, and associate it to <paramref name="parent"/>
        /// </summary>
        /// <param name="kitchenObjectSO"></param>
        /// <param name="parent"></param>
        public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
        {
            GameObject kitchenObjectGameObject = GameObject.Instantiate(original: kitchenObjectSO.Prefab);
            if (kitchenObjectGameObject.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject))
            {
                kitchenObject.setKitchenObjectParent(parent);
            }
        }
    }
}
