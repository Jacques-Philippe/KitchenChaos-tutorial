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

        public void Interact()
        {
            Debug.Log("Interact!");
            Instantiate(mKitchenObjectSO.Prefab, mCounterTopPoint.position, rotation: Quaternion.identity);
            Debug.Log(mKitchenObjectSO.Name);
        }
    }

}