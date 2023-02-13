using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    [CreateAssetMenu()]
    public class AudioClipReferencesSO : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFailed;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickup;
        public AudioClip[] sizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}
