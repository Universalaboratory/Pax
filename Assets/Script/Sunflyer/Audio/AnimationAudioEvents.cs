using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class AnimationAudioEvents : MonoBehaviour
    {
        [System.Serializable]
        protected struct AnimationEventSetup
        {
            public string Name;
            public AudioInstanceReference EventInstance;
        }

        [SerializeField] private List<AnimationEventSetup> Setups = new List<AnimationEventSetup>();

        public void PlayAudio(string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;
            var setup = Setups.Find(s => s.Name == eventName);

            setup.EventInstance.PlayAudio();
        }

        public void SetParameterAndPlayAudio(string eventName, int parameterId, int parameterValue)
        {
            if (string.IsNullOrEmpty(eventName)) return;
            var setup = Setups.Find(s => s.Name == eventName);
            setup.EventInstance.SetParameterByName(parameterId, parameterValue);
            PlayAudio(eventName);
        }

        private void OnDestroy()
        {
            Setups.ForEach(s => AudioManager.Instance.ReleaseInstance(s.EventInstance.AudioInstance));
        }
    }
}