using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlayerSound : MonoBehaviour
    {

        [SerializeField] private Player player;

        private float footstepTimer = 0.0f;
        private float stepInterval = 0.3f;

        private void Update()
        {
            if (!player.IsWalking)
            {
                footstepTimer = 0.0f;
            }
            else
            {
                footstepTimer += Time.deltaTime;
                if (footstepTimer >= stepInterval)
                {
                    this.PlaySound();
                    footstepTimer = 0.0f;
                }
            }
        }

        private void PlaySound()
        {
            SoundManager.Instance.PlayFootstepSound(position: this.transform.position);
        }



    }

}