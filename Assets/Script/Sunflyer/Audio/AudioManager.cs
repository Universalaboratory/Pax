using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using Sunflyer.Utilities;

namespace Sunflyer.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public List<EventInstance> CurrentInstances = new List<EventInstance>();

        private Bus _sfxBus, _musBus, _masterBus;
        public const string BusMasterSave = "Master", BusSfxSave = "Sfx", BusMusSave = "Music";

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            _musBus = RuntimeManager.GetBus("bus:/" + BusMusSave);
            _masterBus = RuntimeManager.GetBus("bus:/");
            _sfxBus = RuntimeManager.GetBus("bus:/" + BusSfxSave);
            Load();
        }

        public void PlayOneShot(EventReference eventReference, Vector3 position = new Vector3())
        {
            RuntimeManager.PlayOneShot(eventReference, position);
        }

        public EventInstance CreateInstance(EventReference eventReference)
        {
            var instance = RuntimeManager.CreateInstance(eventReference);

            CurrentInstances.Add(instance);
            return instance;
        }

        public void ChangeVolume(string bus, float value)
        {
            if (BusSfxSave == bus)
            {
                _sfxBus.setVolume(value);
            }
            else if (BusMusSave == bus)
            {
                _musBus.setVolume(value);
            }
            else
            {
                _masterBus.setVolume(value);
            }
            PlayerPrefs.SetFloat(bus, value);
        }

        public float GetVolume(string bus)
        {
            float busVolume = 0;
            float finalBusVolume = 0;

            if (BusSfxSave == bus)
            {
                _sfxBus.getVolume(out busVolume, out finalBusVolume);
            }
            else if (BusMusSave == bus)
            {
                _musBus.getVolume(out busVolume, out finalBusVolume);
            }
            else
            {
                _masterBus.getVolume(out busVolume, out finalBusVolume);
            }

            return busVolume;
        }

        public void ReleaseInstance(EventInstance instance)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();

            if (CurrentInstances.Contains(instance))
            {
                CurrentInstances.Remove(instance);
            }
        }

        public void Clean()
        {
            for (int i = CurrentInstances.Count - 1; i >= 0; i--)
            {
                ReleaseInstance(CurrentInstances[i]);
            }
        }

        private void OnDestroy()
        {
            Clean();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(BusMasterSave))
            {
                _masterBus.setVolume(PlayerPrefs.GetFloat(BusMasterSave));
            }

            if (PlayerPrefs.HasKey(BusMusSave))
            {
                _musBus.setVolume(PlayerPrefs.GetFloat(BusMusSave));
            }

            if (PlayerPrefs.HasKey(BusSfxSave))
            {
                _sfxBus.setVolume(PlayerPrefs.GetFloat(BusSfxSave));
            }
        }
    }
}