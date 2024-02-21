using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrikinCore.Sfx
{
    [CreateAssetMenu]
    public class SoundClip : ScriptableObject
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume = 1;
        public bool pitchable = true;

        [Flags]
        public enum TypeOfSoundEffect
        {
            High = 1,
            Med = 2,
            Low = 4,
            Music = 8
        }

        //todo: see if this still works how intended
        [EnumToggleButtons] public TypeOfSoundEffect priority;

        public float pitchRangeMin = 0.95f, pitchRangeMax = 1.05f;
    }
}