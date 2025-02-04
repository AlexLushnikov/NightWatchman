using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    public class SoundsService : MonoBehaviour, ISoundsService
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private List<AudioClip> _musicClips;

        private int _currentMusicIndex;
        
        private void Start()
        {
            if (_musicClips.Count > 0 && _musicSource != null)
            {
                PlayClip(_currentMusicIndex);
            }
        }

        private void Update()
        {
            if (!_musicSource.isPlaying && _musicClips.Count > 0)
            {
                PlayNextClip();
            }
        }
        private void PlayClip(int index)
        {
            if (index >= 0 && index < _musicClips.Count)
            {
                _musicSource.clip = _musicClips[index];
                _musicSource.Play();
            }
        }

        private void PlayNextClip()
        {
            _currentMusicIndex = (_currentMusicIndex + 1) % _musicClips.Count;
            PlayClip(_currentMusicIndex);
        }
        
        public void PlayCorrect()
        {
            
        }

        public void PlayIncorrect()
        {
            
        }

        public void SetEffectVolume(float volume)
        {
            _soundSource.volume = volume;
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = volume;
        }
    }
}