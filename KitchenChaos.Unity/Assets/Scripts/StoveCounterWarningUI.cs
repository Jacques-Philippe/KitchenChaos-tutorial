using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class StoveCounterWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        /// <summary>
        /// Timer used to control the play frequency of the warning sound
        /// </summary>
        private float soundTimer = 0.0f;
        /// <summary>
        /// Delay between each warning sound, in seconds
        /// </summary>
        private float soundDelay = 0.25f;

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
                //Play the warning sound every soundDelay seconds
                this.soundTimer += Time.deltaTime;
                if (this.soundTimer >= this.soundDelay)
                {
                    //This sound isn't really heard unless it's close to the camera
                    SoundManager.Instance.PlayWarningSound(position: Camera.main.transform.position);
                    this.soundTimer = 0.0f;
                }
            }
            else
            {
                this.Hide();
                this.soundTimer = 0.0f;
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
