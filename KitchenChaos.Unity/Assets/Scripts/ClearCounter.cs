using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ClearCounter : BaseCounter
    {
        /// <summary>
        /// A reference to the <see cref="KitchenObjectSO"/> object which should drive this table's functionality
        /// </summary>
        [SerializeField] private KitchenObjectSO mKitchenObjectSO;
        

        public override void Interact(Player player)
        {
            Debug.Log("Interact!");
            KitchenObject kitchenObject = this.GetKitchenObject();
            //if there is no kitchen object, spawn it
            if (kitchenObject == null)
            {
                GameObject kitchenGameObj = Instantiate(original: mKitchenObjectSO.Prefab);
                if (kitchenGameObj.TryGetComponent<KitchenObject>(out KitchenObject _kitchenObject))
                {
                    _kitchenObject.setKitchenObjectParent(this);
                }
            }
            //else if there is a kitchen object, give it to the player
            else
            {
                kitchenObject.setKitchenObjectParent(player);
            }

        }



    }

}