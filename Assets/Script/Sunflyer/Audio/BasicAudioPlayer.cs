using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class BasicAudioPlayer : MonoBehaviour
    {
        [SerializeField] protected AudioPlayer Audio;

        public virtual void PlayAudio()
        {
            Audio.PlayAudio();
        }
    }
}