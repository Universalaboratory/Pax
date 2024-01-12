using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class CombatAudio : MonoBehaviour
    {
        [SerializeField] private string MusicParamenter = "Menu Change";
        private bool _changedMusic;

        public void ChangeCombatMusic()
        {
            if (_changedMusic || MusicPlayer.AudioInstance == null)
                return;

            MusicPlayer.AudioInstance.Instance.setParameterByName(MusicParamenter, 1);
        }

    }
}