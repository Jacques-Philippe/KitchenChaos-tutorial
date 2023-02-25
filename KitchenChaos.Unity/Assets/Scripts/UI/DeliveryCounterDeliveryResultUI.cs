using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class DeliveryCounterDeliveryResultUI : MonoBehaviour
    {
        [Header("Initialization")]

        [Tooltip("The color for the background to have on delivery success")]
        [SerializeField] private Color successColor;
        
        [Tooltip("The color for the background to have on delivery failure")]
        [SerializeField] private Color failureColor;



        [Tooltip("The text to be shown on delivery success")]
        [SerializeField] private string successText = "Delivery\nCompleted";
        
        [Tooltip("The text to be shown on delivery failure")]
        [SerializeField] private string failureText = "Delivery\nFailed";
        


        [Tooltip("The icon to be shown on delivery success")]
        [SerializeField] private Sprite successIcon;
        
        [Tooltip("The icon to be shown on delivery success")]
        [SerializeField] private Sprite failureIcon;

        [Header("UI references")]

        [Tooltip("The icon next to the text, to be changed with respect to delivery success status")]
        [SerializeField] private Image icon;
        
        [Tooltip("The text on the UI card, to be changed with respect to delivery success status")]
        [SerializeField] private TextMeshProUGUI text;

        [Tooltip("The background of the UI card to be changed with respect to delivery success status")]
        [SerializeField] private Image background;

        private float hideTimer = 0.0f;
        private const float HIDE_AFTER = 10.0f;
        private bool shouldHideAfterDelay = false;

        void Start ()
        {
            DeliveryManager.Instance.OnOrderSuccess += DeliveryManager_OnOrderSuccess;
            DeliveryManager.Instance.OnOrderFailure += DeliveryManager_OnOrderFailure;

            this.Hide();
        }

        private void Update()
        {
            if (shouldHideAfterDelay)
            {
                hideTimer += Time.deltaTime;
                if (hideTimer >= HIDE_AFTER)
                {
                    this.hideTimer = 0.0f;
                    this.shouldHideAfterDelay = false;
                    this.Hide();
                }
            }
        }

        private void DeliveryManager_OnOrderFailure(object sender, System.EventArgs e)
        {
            Debug.Log("Hello failure?");
            //if we're interrupting a UI display that hasn't reset yet, then hide the display before continuing
            if (this.hideTimer != 0.0f)
            {
                this.Hide();
            }

            this.ShowFailure();
            this.Show();

            this.shouldHideAfterDelay = true;
            this.hideTimer = 0.0f;
        }

        private void DeliveryManager_OnOrderSuccess(object sender, System.EventArgs e)
        {
            Debug.Log("Hello success?");
            //if we're interrupting a UI display that hasn't reset yet, then hide the display before continuing
            if (this.hideTimer != 0.0f)
            {
                this.Hide();
            }

            this.ShowSuccess();
            this.Show();

            this.shouldHideAfterDelay = true;
            this.hideTimer = 0.0f;
        }

        void ShowSuccess()
        {
            this.icon.sprite = successIcon;
            this.text.text = successText;
            this.background.color = successColor;
        }

        void ShowFailure()
        {
            this.icon.sprite = failureIcon;
            this.text.text = failureText;
            this.background.color = failureColor;
        }

        void Show()
        {
            this.gameObject.SetActive(true);
        }

        void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}