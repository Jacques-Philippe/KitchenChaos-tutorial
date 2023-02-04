using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Player mPlayer;

        private const string IS_WALKING = "IsWalking";
        private Animator mAnimator;


        private void Awake()
        {
            mAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            //if we're moving, set the animator to IsWalking true, else false.
            this.mAnimator.SetBool(IS_WALKING, this.mPlayer.IsWalking);
        }


    }
}
