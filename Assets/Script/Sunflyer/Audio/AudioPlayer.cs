using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Sunflyer.Audio
{
    [System.Serializable]
    public class AudioPlayer
    {
        public EventReference Reference => AudioReference;
        [SerializeField] protected EventReference AudioReference;

        public virtual void PlayAudio()
        {
            if (AudioReference.IsNull) return;

            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlayOneShot(AudioReference);
            }
            else
            {
                RuntimeManager.PlayOneShot(AudioReference);
            }
        }
    }
}