using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ordersDeliveredText;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged; ;

            this.Hide();
        }

        private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
        {
            if (e.newState == GameManager.State.GAME_OVER)
            {
                int ordersDelivered = DeliveryManager.Instance.GetOrdersDelivered();
                this.ordersDeliveredText.text = ordersDelivered.ToString();
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
