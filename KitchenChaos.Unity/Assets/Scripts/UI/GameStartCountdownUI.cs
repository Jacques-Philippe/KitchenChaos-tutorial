using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged; ;

            this.Show();
        }

        private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
        {
            if (e.newState == GameManager.State.GAME_PLAYING)
            {
                this.Hide();
            }
        }

        private void Update()
        {
            if (GameManager.Instance.state == GameManager.State.GAME_STARTING)
            {
                countdownText.text = Mathf.Ceil(GameManager.Instance.GetGameStartingTimer()).ToString();
            }

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
