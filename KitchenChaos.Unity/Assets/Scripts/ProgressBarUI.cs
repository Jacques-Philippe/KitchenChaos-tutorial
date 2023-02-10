using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject mGameObjectHasProgress;
        [SerializeField] private Image mFill;

        private IHasProgress mHasProgress;

        private void Start()
        {
            
            if (this.mGameObjectHasProgress.TryGetComponent<IHasProgress>(out IHasProgress hasProgress))
            {
                mHasProgress = hasProgress;
                mHasProgress.OnProgressChanged += MCuttingCounter_OnCutChanged;
            }
            else
            {
                Debug.LogError($"{mGameObjectHasProgress.name} should implement IHasProgress");
            }
            

            mFill.fillAmount = 0.0f;
            this.Hide();
        }

        private void MCuttingCounter_OnCutChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            this.SetFillAmount(normalizedProgress: e.normalizedProgress);
        }


        private void SetFillAmount(float normalizedProgress)
        {
            this.mFill.fillAmount = normalizedProgress;
            if (normalizedProgress == 0.0f || normalizedProgress == 1.0f)
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
