using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    /// <summary>
    /// A class to manage the visual display of the selected counter
    /// </summary>
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private GameObject mCounterVisual;
        private void Show()
        {
            this.mCounterVisual.SetActive(true);
        }
        private void Hide()
        {
            this.mCounterVisual.SetActive(false);
        }
    }

}