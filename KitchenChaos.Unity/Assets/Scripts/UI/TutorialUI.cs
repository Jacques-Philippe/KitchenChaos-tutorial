using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private TextMeshProUGUI interactAltText;
        [SerializeField] private TextMeshProUGUI pauseText;
        [SerializeField] private TextMeshProUGUI gamepadInteractText;
        [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
        [SerializeField] private TextMeshProUGUI gamepadPauseText;


        // Start is called before the first frame update
        void Start()
        {
            //On input rebound, update tutorial UI to match the new controller layout
            GameInput.Instance.OnRebindingComplete += GameInput_OnRebindingComplete;
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

            //initialize all the text fields with the key bindings
            this.UpdateVisual();

            this.Show();
        }

        private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
        {
            if (GameManager.Instance.IsGameStarting())
            {
                this.Hide();
            }
        }

        private void GameInput_OnRebindingComplete(object sender, System.EventArgs e)
        {
            this.UpdateVisual();
        }

        private void UpdateVisual()
        {
            moveUpText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Up);
            moveDownText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Down);
            moveLeftText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Left);
            moveRightText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Move_Right);
            interactText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Interact);
            interactAltText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Interact_Alt);
            pauseText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Pause);
            gamepadInteractText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Gamepad_Interact);
            gamepadInteractAltText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Gamepad_Interact_Alt);
            gamepadPauseText.text = GameInput.Instance.GetBindingDisplayString(GameInput.Bindings.Gamepad_Pause);
        }

        private void Show()
        {
            this.gameObject.SetActive(true);
        }

        private void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
