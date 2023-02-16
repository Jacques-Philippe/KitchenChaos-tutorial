using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { private set; get; }

        [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There should be only one instance of the SoundManager");
            }
        }

        private void Start()
        {
            DeliveryManager.Instance.OnOrderSuccess += DeliveryManager_OnOrderSuccess;
            DeliveryManager.Instance.OnOrderFailure += DeliveryManager_OnOrderFailure;
            CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
            TrashCounter.OnAnyKitchenObjectTrashed += TrashCounter_OnAnyKitchenObjectTrashed;
            Player.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;
            BaseCounter.OnSomethingPutDown += BaseCounter_OnSomethingPutDown;

        }

        private void TrashCounter_OnAnyKitchenObjectTrashed(object sender, System.EventArgs e)
        {
            Vector3 position = (sender as TrashCounter).transform.position;
            this.PlaySound(clipArray: audioClipReferencesSO.trash, position);
        }

        private void BaseCounter_OnSomethingPutDown(object sender, System.EventArgs e)
        {
            Vector3 position = (sender as BaseCounter).transform.position;
            this.PlaySound(clipArray: audioClipReferencesSO.objectDrop, position);
        }

        private void Player_OnPickedUpSomething(object sender, System.EventArgs e)
        {
            this.PlaySound(clipArray: audioClipReferencesSO.objectPickup, position: Player.Instance.transform.position);
        }

        private void Player_OnMovement(object sender, System.EventArgs e)
        {
            this.PlaySound(clipArray: audioClipReferencesSO.footstep, position: Player.Instance.transform.position);
        }

        private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
        {
            Vector3 position = (sender as CuttingCounter).transform.position;
            this.PlaySound(clipArray: this.audioClipReferencesSO.chop, position);
        }

        private void DeliveryManager_OnOrderFailure(object sender, System.EventArgs e)
        {
            this.PlaySound(clipArray: this.audioClipReferencesSO.deliveryFailed, position: DeliveryCounter.Instance.transform.position);
        }

        private void DeliveryManager_OnOrderSuccess(object sender, System.EventArgs e)
        {
            this.PlaySound(clipArray: this.audioClipReferencesSO.deliverySuccess, position: DeliveryCounter.Instance.transform.position);
        }

        private void PlaySound(AudioClip[] clipArray, Vector3 position, float volume = 1.0f)
        {
            int index = Random.Range(minInclusive: 0, maxExclusive: clipArray.Length);
            AudioSource.PlayClipAtPoint(clip: clipArray[index], position: position, volume: volume);
        }
        
        private void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f)
        {
            AudioSource.PlayClipAtPoint(clip: clip, position: position, volume: volume);
        }

        public void PlayFootstepSound(Vector3 position)
        {
            this.PlaySound(clipArray: this.audioClipReferencesSO.footstep, position);
        }
    }
}
