using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KitchenChaosTutorial
{

    public class StoveCounterProgressBarFlashUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private const string ANIMATOR_IS_FLASHING = "isFlashing";

        private Animator animator;

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
            animator = GetComponent<Animator>();

        }


        //given the state of the StoveCounter (food is 50% to burnt)
        //play the warning animation
        //play the warning sound
        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            //Start warning the player for food 50% burnt
            float burnThreshold = 0.5f;
            bool isBurning = stoveCounter.IsBurning() && e.normalizedProgress >= burnThreshold;

            //flash red
            this.animator.SetBool(ANIMATOR_IS_FLASHING, isBurning);
        }
    }
}
