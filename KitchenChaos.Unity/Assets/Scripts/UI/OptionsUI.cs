using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private PauseUI pauseUI;

        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;

        [SerializeField] private Transform pressToRebindKeyUI;

        [SerializeField] private Button moveUpRebindingButton;

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

            this.closeButton.onClick.AddListener(() =>
            {
                this.Hide();
                pauseUI.Show();
            });

            this.moveUpRebindingButton.onClick.AddListener(() => {
                this.ShowPressToRebindKeyUI();
                //Listen for rebinding key
                GameInput.Instance.RebindBinding(GameInput.Bindings.Move_Up, onBindingComplete: HidePressToRebindKeyUI);
            });


            //Hide the options UI for game unpaused (i.e. player hits Esc)
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            //Reset text
            this.UpdateSoundEffectsText();
            this.UpdateMusicText();

            this.Hide();
            this.HidePressToRebindKeyUI();
        }

        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            this.Hide();
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

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        private void Hide()
        {
            this.gameObject.SetActive(false);
        }

        private void ShowPressToRebindKeyUI()
        {
            this.pressToRebindKeyUI.gameObject.SetActive(true);
        }

        private void HidePressToRebindKeyUI()
        {
            this.pressToRebindKeyUI.gameObject.SetActive(false);
        }
    }

}