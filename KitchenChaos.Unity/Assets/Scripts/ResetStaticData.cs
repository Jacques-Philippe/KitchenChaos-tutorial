using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    /// <summary>
    /// Class responsible for resetting static data left over from scene reloads
    /// </summary>
    public class ResetStaticData : MonoBehaviour
    {
        private void Awake()
        {
            BaseCounter.ResetStaticData();
            CuttingCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}
