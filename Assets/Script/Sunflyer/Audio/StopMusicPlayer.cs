using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class StopMusicPlayer : MonoBehaviour
    {
        private MusicPlayer MusicPlayer;

        private void Start()
        {
            TryGetMusicPlayer();
        }

        private void OnEnable()
        {
            TryGetMusicPlayer();
        }

        private void TryGetMusicPlayer()
        {
            if (MusicPlayer) return;
            MusicPlayer = FindObjectOfType<MusicPlayer>();
        }

        public void StopMusic()
        {
            TryGetMusicPlayer();
            if (!MusicPlayer) return;
            MusicPlayer.StopAudio();
        }
    }
}