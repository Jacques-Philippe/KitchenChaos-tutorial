using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        /// <summary>
        /// The name of the trigger for the animation to play for each number changed
        /// </summary>
        private const string NUMBER_ANIMATION = "NumberAnimation";

        private Animator animator;
        private int lastTimerInt = -1;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

            this.animator = GetComponent<Animator>();

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
                int timerInt = Mathf.CeilToInt(GameManager.Instance.GetGameStartingTimer());
                countdownText.text = timerInt.ToString();
                //if the number changed, fire the animation
                if (timerInt != lastTimerInt)
                {
                    animator.SetTrigger(NUMBER_ANIMATION);
                    SoundManager.Instance.PlayCountdownSound();
                    lastTimerInt = timerInt;
                }
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
