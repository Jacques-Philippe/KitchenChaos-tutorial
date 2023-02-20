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
        private float volume = VOLUME_DEFAULT_VALUE;
        /// <summary>
        /// The default value of the volume
        /// </summary>
        private const float VOLUME_DEFAULT_VALUE = 0.6f;
        /// <summary>
        /// Reference to the audiosource component the game music is attached to
        /// </summary>
        private AudioSource audioSource;
        private const string PLAYERPREFS_MUSIC_VOLUME = "Music Volume";

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
            this.volume = PlayerPrefs.GetFloat(key: PLAYERPREFS_MUSIC_VOLUME, defaultValue: VOLUME_DEFAULT_VALUE);
            this.audioSource.volume = this.volume;
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

            PlayerPrefs.SetFloat(key: PLAYERPREFS_MUSIC_VOLUME, value: this.volume);
            PlayerPrefs.Save();
        }

        public float GetVolume() { return this.volume; }
    }

}