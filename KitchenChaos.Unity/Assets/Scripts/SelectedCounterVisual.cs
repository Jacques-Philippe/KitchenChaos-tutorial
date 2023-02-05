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

        private void Show()
        {
            this.gameObject.SetActive(true);
        }
        private void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}