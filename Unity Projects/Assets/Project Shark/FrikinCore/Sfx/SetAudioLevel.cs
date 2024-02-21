using FrikinCore.GameInformation;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FrikinCore.Sfx
{
    public class SetAudioLevel : MonoBehaviour
    {
        public AudioMixer MasterMixer;

        [SerializeField] GameSettings _gameSettings;

        [SerializeField] Slider _master, _music, _soundFX;

        void Awake()
        {
            _master.value = _gameSettings.MasterVolume;
            _music.value = _gameSettings.MusicVolume;
            _soundFX.value = _gameSettings.SoundVolume;

            UpdateAllSlidersToCurrentValues();
        }

        public void UpdateAllSlidersToCurrentValues()
        {
            SetMasterLevel(_gameSettings.MasterVolume);
            SetMusicLevel(_gameSettings.MusicVolume);
            SetSoundLevel(_gameSettings.SoundVolume);
        }

        public void SetMasterLevel(float masterLvl)
        {
            _gameSettings.MasterVolume = masterLvl;
            _master.value = masterLvl;
            AudioListener.volume = masterLvl;
        }

        public void SetMusicLevel(float musicLvl)
        {
            _gameSettings.MusicVolume = musicLvl;
            _music.value = musicLvl;
            MasterMixer.SetFloat("MusicVol", Mathf.Log10(musicLvl) * 20);
        }

        public void SetSoundLevel(float SoundLevel)
        {
            _gameSettings.SoundVolume = SoundLevel;
            _soundFX.value = SoundLevel;
            MasterMixer.SetFloat("SoundVol", Mathf.Log10(SoundLevel) * 20);
        }
    }
}
