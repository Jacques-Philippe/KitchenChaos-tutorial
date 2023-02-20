using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;

        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;

        private const string soundEffectsButtonTextPrefix = "Sound Effects: ";
        private const string musicButtonTextPrefix = "Music: ";

        private float sfxUpdateIncrement = 0.1f;
        private float musicUpdateIncrement = 0.1f;

        private void Start()
        {
            this.soundEffectsButton.onClick.AddListener(() =>
            {
                //Affect volume
                SoundManager.Instance.UpdateVolume(sfxUpdateIncrement);
                //Reset text
                this.UpdateSoundEffectsText();
            });

            this.musicButton.onClick.AddListener(() =>
            {
                //Affect volume
                MusicManager.Instance.UpdateVolume(musicUpdateIncrement);
                //Reset text
                this.UpdateMusicText();
            });

            //Reset text
            this.UpdateSoundEffectsText();
            this.UpdateMusicText();
        }

        /// <summary>
        /// Helper to update the contents of <see cref="soundEffectsText"/> with <see cref="SoundManager"/> volume state
        /// </summary>
        private void UpdateSoundEffectsText()
        {
            //multiply volume by 10 for a nicer visual format
            int volume = (int)Mathf.Round(SoundManager.Instance.GetVolume() * 10.0f);
            this.soundEffectsText.text = soundEffectsButtonTextPrefix + volume;
        }

        /// <summary>
        /// Helper to update the contents of <see cref="musicText"/> with <see cref="MusicManager"/> volume state
        /// </summary>
        private void UpdateMusicText()
        {
            //multiply volume by 10 for a nicer visual format
            int volume = (int)Mathf.Round(MusicManager.Instance.GetVolume() * 10.0f);
            this.musicText.text = musicButtonTextPrefix + volume;
        }
    }

}