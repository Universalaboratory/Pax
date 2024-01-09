using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class StaticAudioInstance
    {
        public EventInstance Instance;
        public EventReference Reference;
    }

    public class MusicPlayer : MonoBehaviour
    {
        public static StaticAudioInstance AudioInstance;
        public AudioInstanceReference InstanceReference => Audio;
        [SerializeField] protected AudioInstanceReference Audio;
        [SerializeField] protected bool SetGlobalInstance;
        [SerializeField] protected bool PlayOnStart = true;
        [SerializeField] private bool SetParamaterOnStart;
        [SerializeField] private int ParamID, ParamValue;

        private void Start()
        {
            InitStart();
        }

        protected virtual void InitStart()
        {
            if (AudioInstance == null)
            {
                AudioInstance = new StaticAudioInstance();
            }

            if (SetParamaterOnStart)
            {
                if (!Audio.Reference.IsNull &&
                AudioInstance.Reference.Equals(Audio.Reference) && 
                AudioInstance.Instance.isValid())
                {
                    Audio.SetupInstance(AudioInstance.Instance);
                    Audio.SetParameterByName(ParamID, ParamValue);
                }
            }

            if (!Audio.AudioInstance.isValid())
                Audio.SetupInstance();

            if (!PlayOnStart || !CanPlayAudio()) return;

            PlayAudio();
        }

        public virtual void PlayAudio()
        {
            if (SetGlobalInstance && AudioInstance.Instance.isValid() && !AudioInstance.Equals(null))
            {
                AudioManager.Instance.ReleaseInstance(AudioInstance.Instance);
            }

            Audio.PlayAudio();

            if (SetGlobalInstance)
            {
                AudioInstance.Instance = Audio.AudioInstance;
                AudioInstance.Reference = Audio.Reference;
            }
        }

        protected virtual bool CanPlayAudio()
        {
            return Audio.AudioInstance.isValid() &&
            !AudioInstance.Reference.Equals(Audio.Reference);
        }

        public void StopAudio()
        {
            Audio.StopAudio();
        }

        private void OnDestroy()
        {
            if (!SetGlobalInstance)
                StopAudio();
        }
    }
}