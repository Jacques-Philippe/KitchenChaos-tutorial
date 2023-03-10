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
        /// <summary>
        /// A reference to the <see cref="ClearCounter"/> this <see cref="SelectedCounterVisual"/> is a child of
        /// </summary>
        [SerializeField] private BaseCounter mCounter;
        /// <summary>
        /// The gameobject used to display the selected counter. <br />
        /// We expect this gameobject to be displayed for counter selected, and hidden for counter unselected.
        /// </summary>
        [SerializeField] private GameObject[] mCounterVisual;


        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        /// <summary>
        /// Event fired on player selected counter changed, if the newly selected counter is this one, show this counter is selected. Else, show this counter is unselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
        {
            if (e.selectedCounter == this.mCounter)
            {
                this.DisplayAsSelected();
            }
            else
            {
                this.DisplayAsUnselected();
            }
        }

        /// <summary>
        /// Helper function to show the counter as selected
        /// </summary>
        private void DisplayAsSelected()
        {
            foreach(var item in mCounterVisual)
            {
                item.SetActive(true);
            }
        }
        /// <summary>
        /// Helper function to show the counter as unselected
        /// </summary>
        private void DisplayAsUnselected()
        {
            foreach (var item in mCounterVisual)
            {
                item.SetActive(false);
            }
        }
    }

}