using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    /// <summary>
    /// A class to manage all game SFX
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { private set; get; }

        [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

        private const string PLAYERPREFS_SFX_VOLUME = "SFX Volume";

        /// <summary>
        /// The volume of the SFX
        /// </summary>
        private float volume = VOLUME_DEFAULT_VALUE;
        /// <summary>
        /// The default value of the volume
        /// </summary>
        private const float VOLUME_DEFAULT_VALUE = 0.3f;

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

            this.volume = PlayerPrefs.GetFloat(key: PLAYERPREFS_SFX_VOLUME, defaultValue: VOLUME_DEFAULT_VALUE);
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

        private void PlaySound(AudioClip[] clipArray, Vector3 position, float volumeMultiplier = 1.0f)
        {
            int index = Random.Range(minInclusive: 0, maxExclusive: clipArray.Length);
            this.PlaySound(clip: clipArray[index], position: position, volumeMultiplier: volumeMultiplier);
        }
        
        private void PlaySound(AudioClip clip, Vector3 position, float volumeMultiplier = 1.0f)
        {
            AudioSource.PlayClipAtPoint(clip: clip, position: position, volume: volumeMultiplier * this.volume);
        }

        public void PlayFootstepSound(Vector3 position)
        {
            this.PlaySound(clipArray: this.audioClipReferencesSO.footstep, position);
        }

        public void PlayCountdownSound()
        {
            this.PlaySound(clipArray: audioClipReferencesSO.warning, Camera.main.transform.position);
        }

        /// <summary>
        /// Helper to update the SFX volume externally by <paramref name="increment"/> <br />
        /// If <see cref="volume"/> exceeds 1, resets to 0.
        /// </summary>
        /// <param name="increment"></param>
        public void UpdateVolume(float increment)
        {
            this.volume += increment;
            if (this.volume > 1.0f)
            {
                this.volume = 0.0f;
            }

            PlayerPrefs.SetFloat(key: PLAYERPREFS_SFX_VOLUME, value: this.volume);
            PlayerPrefs.Save();
        }

        public float GetVolume() { return this.volume; }
    }
}
