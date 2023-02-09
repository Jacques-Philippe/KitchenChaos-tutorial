using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    [CreateAssetMenu()]
    public class FryRecipeSO : ScriptableObject
    {
        /// <summary>
        /// The input kitchen object to cook
        /// </summary>
        public KitchenObjectSO inputKitchenObject;
        /// <summary>
        /// The output cooked kitchen object
        /// </summary>
        public KitchenObjectSO outputKitchenObject;
        /// <summary>
        /// The time it takes to fry the input kitchen object to get output kitchen object
        /// </summary>
        public float TimeToFry;

    }

}