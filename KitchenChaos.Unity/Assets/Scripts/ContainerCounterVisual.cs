using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ContainerCounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";

        [SerializeField] private Animator mAnimator;

        [SerializeField] private ContainerCounter mContainerCounter;

        private void OnEnable()
        {
            this.mContainerCounter.ContainerOpened += ToggleOpenClose;
        }

        private void ToggleOpenClose(object sender, EventArgs e)
        {
            this.mAnimator.SetTrigger(OPEN_CLOSE);
        }
    }
}
