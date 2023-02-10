using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class CuttingCounterVisual : MonoBehaviour
    {
        [SerializeField] private Animator mAnimator;
        [SerializeField] private CuttingCounter mCuttingCounter;
        
        private const string CUT = "Cut";

        private void Start()
        {
            mCuttingCounter.OnCut += MCuttingCounter_OnCut;
        }

        private void MCuttingCounter_OnCut(object sender, System.EventArgs e)
        {
            this.mAnimator.SetTrigger(CUT);
        }



    }

}