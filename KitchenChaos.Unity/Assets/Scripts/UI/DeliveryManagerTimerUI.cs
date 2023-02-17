using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class DeliveryManagerTimerUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private void Update()
        {
            if (GameManager.Instance.state == GameManager.State.GAME_PLAYING)
            {
                float percentage = GameManager.Instance.GetGamePlayingTimerNormalized();
                fillImage.fillAmount = percentage;
            }
        }
    }

}