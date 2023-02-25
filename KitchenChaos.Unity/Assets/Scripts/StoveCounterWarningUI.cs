using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class StoveCounterWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
            this.Hide();
        }


        //given the state of the StoveCounter (food is 50% to burnt)
        //play the warning animation
        //play the warning sound
        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            //Start warning the player for food 50% burnt
            float burnThreshold = 0.5f;
            if (stoveCounter.IsBurning() && e.normalizedProgress >= burnThreshold)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        void Show()
        {
            this.gameObject.SetActive(true);
        }

        void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
