using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    /// <summary>
    /// A class to contain Slice recipe data
    /// </summary>
    [CreateAssetMenu()]
    public class SliceRecipeSO : ScriptableObject
    {
        /// <summary>
        /// The incoming KitchenObjectSO (we expect this to be a whole item)
        /// </summary>
        public KitchenObjectSO input;
        /// <summary>
        /// The outgoing KitchenObjectSO (we expect this to be a sliced version of the input)
        /// </summary>
        public KitchenObjectSO output;
    }
}
