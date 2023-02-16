using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            stoveCounter.OnStoveOn += StoveCounter_OnStoveOn;
            stoveCounter.OnFoodBurned += StoveCounter_OnFoodBurned;
            stoveCounter.OnFoodRemoved += StoveCounter_OnFoodRemoved;
        }

        private void StoveCounter_OnFoodRemoved(object sender, System.EventArgs e)
        {
            this.StopPlayingSound();
        }

        private void StoveCounter_OnFoodBurned(object sender, System.EventArgs e)
        {
            this.StopPlayingSound();
        }

        private void StoveCounter_OnStoveOn(object sender, System.EventArgs e)
        {
            audioSource.Play();
        }

        private void StopPlayingSound()
        {
            if (this.audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
