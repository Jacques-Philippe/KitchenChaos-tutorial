using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    /// <summary>
    /// A class to contain Kitchen Object data
    /// </summary>
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        /// <summary>
        /// The prefab which should be spawned for the given kitchen object
        /// </summary>
        public GameObject Prefab;
        /// <summary>
        /// The sprite which should be used to represent the given kitchen object
        /// </summary>
        public Sprite Sprite;
        /// <summary>
        /// The name which should be used to represent the given kitchen object
        /// </summary>
        public string Name;
    }
}
