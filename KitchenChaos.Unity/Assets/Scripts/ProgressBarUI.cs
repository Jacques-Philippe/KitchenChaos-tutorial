using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounter mCuttingCounter;
        [SerializeField] private Image mFill;

        private void Start()
        {
            mCuttingCounter.OnCutProgressChanged += MCuttingCounter_OnCutChanged;

            mFill.fillAmount = 0.0f;
            this.Hide();
        }

        private void MCuttingCounter_OnCutChanged(object sender, CuttingCounter.CutChangedEventArgs e)
        {
            this.SetFillAmount(percentage: e.percentage);
        }

        private void SetFillAmount(float percentage)
        {
            this.mFill.fillAmount = percentage;
            if (percentage == 0.0f || percentage == 1.0f)
            {
                this.Hide();
            }
            else
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
