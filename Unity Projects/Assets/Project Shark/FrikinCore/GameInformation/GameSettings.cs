using UnityEngine;

namespace FrikinCore.GameInformation
{
    [CreateAssetMenu(menuName = "Game Settings/Game Settings")]

    public class GameSettings : ScriptableObject
    {
        [SerializeField] FullScreenMode _windowMode;
        [SerializeField] float _masterVolume = 1;
        [SerializeField] float _soundVolume = 1;
        [SerializeField] float _musicVolume = 1;

        public FullScreenMode WindowMode { get; set; }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                _masterVolume = Mathf.Clamp(_masterVolume, 0f, 1f);
            }
        }

        public float SoundVolume
        {
            get => _soundVolume;
            set
            {
                _soundVolume = value;
                _soundVolume = Mathf.Clamp(_soundVolume, 0.0001f, 1f);
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                _musicVolume = Mathf.Clamp(_musicVolume, 0.0001f, 1f);
            }
        }

        public void OnValidate()
        {
            WindowMode = _windowMode;
            MasterVolume = _masterVolume;
            SoundVolume = _soundVolume;
            MusicVolume = _musicVolume;
        }

        //This code below may be used at a later time for screen resolution if needed. Through looking at 
        // info about screen res there is a value saved for the app itself.

        // public void OnEnable()
        // {
        //     Debug.Log("Game Settings Object Loaded!!!");
        // }

        //public void UpdateFullScreenMode() => PlayerSettings.fullScreenMode = WindowMode;
    }
}
