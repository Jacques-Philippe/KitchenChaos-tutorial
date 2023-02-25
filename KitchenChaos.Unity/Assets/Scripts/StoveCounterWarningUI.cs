using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class StoveCounterWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        
        /// <summary>
        /// String identifier for the bool for the transition from idle to warning flash for the warning UI
        /// </summary>
        private const string ANIMATOR_SHOULD_FLASH = "shouldFlash";

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();

            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
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
                this.animator.SetBool(ANIMATOR_SHOULD_FLASH, true);
            }
            else
            {
                this.animator.SetBool(ANIMATOR_SHOULD_FLASH, false);
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
