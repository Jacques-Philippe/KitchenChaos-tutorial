using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class DeliveryManagerUI : MonoBehaviour
    {

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
            this.Hide();
        }
        private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
        {
            if (e.newState == GameManager.State.GAME_PLAYING)
            {
                this.Show();
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
