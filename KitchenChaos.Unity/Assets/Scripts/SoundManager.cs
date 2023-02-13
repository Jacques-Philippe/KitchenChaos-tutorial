using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

        private void Start()
        {
            DeliveryManager.Instance.OnOrderSuccess += DeliveryManager_OnOrderSuccess;
            DeliveryManager.Instance.OnOrderFailure += DeliveryManager_OnOrderFailure;
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
    }
}
