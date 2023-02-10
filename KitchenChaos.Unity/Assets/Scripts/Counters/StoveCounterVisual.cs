using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private GameObject mSizzlingParticles;
        [SerializeField] private GameObject mStoveOnVisual;

        [SerializeField] private StoveCounter mStoveCounter;

        private void Start()
        {
            this.mStoveCounter.OnFoodBurned += MStoveCounter_OnFoodBurned;
            this.mStoveCounter.OnFoodRemoved += MStoveCounter_OnFoodRemoved;
            mStoveCounter.OnStoveOn += MStoveCounter_OnStoveOn;
        }

        private void MStoveCounter_OnFoodRemoved(object sender, System.EventArgs e)
        {
            this.ShowStoveOffVisuals();
        }

        private void MStoveCounter_OnStoveOn(object sender, System.EventArgs e)
        {
            this.ShowStoveOnVisuals();
        }

        private void MStoveCounter_OnFoodBurned(object sender, System.EventArgs e)
        {
            this.ShowStoveOffVisuals();
        }



        private void ShowStoveOnVisuals()
        {
            this.mSizzlingParticles.SetActive(true);
            this.mStoveOnVisual.SetActive(true);
        }

        private void ShowStoveOffVisuals()
        {
            this.mSizzlingParticles.SetActive(false);
            this.mStoveOnVisual.SetActive(false);
        }
    }

}