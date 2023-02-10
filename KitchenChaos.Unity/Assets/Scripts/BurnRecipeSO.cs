using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    [CreateAssetMenu()]
    public class BurnRecipeSO : ScriptableObject
    {
        /// <summary>
        /// The input kitchen object to burn
        /// </summary>
        public KitchenObjectSO inputKitchenObject;
        /// <summary>
        /// The output cooked kitchen object
        /// </summary>
        public KitchenObjectSO outputKitchenObject;
        /// <summary>
        /// The time it takes to burn the input kitchen object to get output kitchen object
        /// </summary>
        public float TimeToBurn;

    }

}