using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace KitchenChaosTutorial
{

    public class MusicManager : MonoBehaviour
    {
        /// <summary>
        /// A public-facing singleton instance of the music manager
        /// </summary>
        public static MusicManager Instance { private set; get; }
        /// <summary>
        /// The volume of the music
        /// </summary>
        private float volume = 0.3f;

        private AudioSource audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There should be only one instance of MusicManager");
            }
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Helper to update the Music volume externally by <paramref name="increment"/> <br />
        /// If <see cref="volume"/> exceeds 1, resets to 0.
        /// </summary>
        /// <param name="increment"></param>
        public void UpdateVolume(float increment)
        {
            this.volume += increment;
            if (this.volume > 1.0f)
            {
                this.volume = 0.0f;
            }

            this.audioSource.volume = this.volume;
        }

        public float GetVolume() { return this.volume; }
    }

}