using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Sunflyer.Audio
{
    [System.Serializable]
    public class AudioInstanceReference : AudioPlayer
    {
        public EventInstance AudioInstance => _audioInstance;

        [Header("Audio Instance")]
        [SerializeField] private bool UseAudioInstance = true;
        [SerializeField] private List<string> Parameters = new List<string>();
        [Header("Configs")]
        [SerializeField] private FMOD.Studio.STOP_MODE StopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT;
        [SerializeField] private bool PlayIfNotPlaying = true, StopIfPlaying = true;

        private EventInstance _audioInstance;

        public void SetupInstance()
        {
            if (AudioManager.Instance && !AudioReference.IsNull && UseAudioInstance)
                _audioInstance = AudioManager.Instance.CreateInstance(AudioReference);
        }

        public void SetupInstance(EventInstance audioInstance)
        {
            _audioInstance = audioInstance;
        }

        public override void PlayAudio()
        {
            if (!UseAudioInstance)
            {
                base.PlayAudio();
                return;
            }

            if (_audioInstance.Equals(null))
            {
                SetupInstance();
            }

            if (_audioInstance.Equals(null)) return;

            if (PlayIfNotPlaying && !PlaybackState().Equals(PLAYBACK_STATE.STOPPED)) return;

            _audioInstance.start();

        }

        public void StopAudio()
        {
            if (_audioInstance.Equals(null) || !UseAudioInstance) return;

            if (StopIfPlaying && !PlaybackState().Equals(PLAYBACK_STATE.PLAYING)) return;

            _audioInstance.stop(StopMode);
        }

        public void SetParameterByName(int paramId, float value, bool ignoreSeekSpeed = false)
        {
            if (Parameters.Count <= paramId) return;
            SetParameterByName(Parameters[paramId], value, ignoreSeekSpeed);
        }

        public void SetParameterByName(string parameter, float value, bool ignoreSeekSpeed = false)
        {
            if (string.IsNullOrEmpty(parameter) || !UseAudioInstance || _audioInstance.Equals(null)) return;

            _audioInstance.setParameterByName(parameter, value, ignoreSeekSpeed);

        }

        public PLAYBACK_STATE PlaybackState()
        {
            PLAYBACK_STATE playback;

            _audioInstance.getPlaybackState(out playback);

            return playback;
        }
    }
}