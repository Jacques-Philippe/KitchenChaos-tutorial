using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button optionsButton;

        [SerializeField] private OptionsUI optionsUI;

        private void Start()
        {
            GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused; ;

            resumeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePause();
            });

            quitButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.MAIN_MENU_SCENE);
            });
            optionsButton.onClick.AddListener(() =>
            {
                this.Hide();
                optionsUI.Show();
            });

            this.Hide();
        }

        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void GameManager_OnGamePaused(object sender, System.EventArgs e)
        {
            this.Show();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        private void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
