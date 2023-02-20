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

        /// <summary>
        /// The button the user clicks when they want to rebind the move up button
        /// </summary>
        [SerializeField] private Button moveUpRebindingButton;
        [SerializeField] private Button moveDownRebindingButton;
        [SerializeField] private Button moveLeftRebindingButton;
        [SerializeField] private Button moveRightRebindingButton;
        [SerializeField] private Button interactRebindingButton;
        [SerializeField] private Button interactAltRebindingButton;
        [SerializeField] private Button pauseRebindingButton;
        /// <summary>
        /// The text element responsible for showing the input mapped to move up
        /// </summary>
        [SerializeField] private TextMeshProUGUI moveUpRebindingKeyText;
        [SerializeField] private TextMeshProUGUI moveDownRebindingKeyText;
        [SerializeField] private TextMeshProUGUI moveLeftRebindingKeyText;
        [SerializeField] private TextMeshProUGUI moveRightRebindingKeyText;
        [SerializeField] private TextMeshProUGUI interactRebindingKeyText;
        [SerializeField] private TextMeshProUGUI interactAltRebindingKeyText;
        [SerializeField] private TextMeshProUGUI pauseRebindingKeyText;

        /// <summary>
        /// The text prefix preceding the number on the SFX button
        /// </summary>
        private const string soundEffectsButtonTextPrefix = "Sound Effects: ";
        private const string musicButtonTextPrefix = "Music: ";
        /// <summary>
        /// The increment by which we increment the SFX volume
        /// </summary>
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

            //Event subscribers for rebinding buttons

            this.moveUpRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Move_Up));
            this.moveDownRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Move_Down));
            this.moveLeftRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Move_Left));
            this.moveRightRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Move_Right));
            this.interactRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Interact));
            this.interactAltRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Interact_Alt));
            this.pauseRebindingButton.onClick.AddListener(() => this.Rebind(GameInput.Bindings.Pause));


            //Hide the options UI for game unpaused (i.e. player hits Esc)
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            //Reset text
            this.UpdateVisual();

            this.Hide();
            this.HidePressToRebindKeyUI();
        }

        /// <summary>
        /// Helper to contain the rebinding flow, for each binding
        /// </summary>
        /// <param name="binding"></param>
        private void Rebind(GameInput.Bindings binding)
        {
            //Show UI telling user to press the key to rebind to
            this.ShowPressToRebindKeyUI();
            //On binding complete, hide the UI
            GameInput.Instance.RebindBinding(binding, onBindingComplete: () =>
            {
                this.HidePressToRebindKeyUI();
                this.UpdateVisual();
            });
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

        /// <summary>
        /// Helper to update the UI from state for all UI elements of the options UI
        /// </summary>
        private void UpdateVisual()
        {
            //multiply volume by 10 for a nicer visual format
            int musicVolume = (int)Mathf.Round(MusicManager.Instance.GetVolume() * 10.0f);
            this.musicText.text = musicButtonTextPrefix + musicVolume;

            //multiply volume by 10 for a nicer visual format
            int sfxVolume = (int)Mathf.Round(SoundManager.Instance.GetVolume() * 10.0f);
            this.soundEffectsText.text = soundEffectsButtonTextPrefix + sfxVolume;

            //Update key bindings UI from GameInput state
            this.moveUpRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Up);
            this.moveDownRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Down);
            this.moveLeftRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Left);
            this.moveRightRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Right);
            this.interactRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Interact);
            this.interactAltRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Interact_Alt);
            this.pauseRebindingKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Pause);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        private void Hide()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// Show the UI responsible for telling the user to press a key in order for their chosen input to be rebound
        /// </summary>
        private void ShowPressToRebindKeyUI()
        {
            this.pressToRebindKeyUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide the UI responsible for telling the user to press a key in order for their chosen input to be rebound
        /// </summary>
        private void HidePressToRebindKeyUI()
        {
            this.pressToRebindKeyUI.gameObject.SetActive(false);
        }
    }

}