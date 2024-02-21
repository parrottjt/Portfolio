using System.Collections;
using UnityEngine;

namespace FrikinCore.Player.Managers
{
    public partial

    class PlayerDataManager
    {
        [Header("Level Bool Testing,will be [HideInInspector] later")]
        public bool kawaiiMode;

        public LevelNames normLevelNames;

        public BitArray normalLevelArray, kawaiiLevelArray;

        public enum LevelNames
        {
            Tutorial,
            TutorialBoss,
            W1L1,
            W1L2,
            W1L3,
            W1Boss,
            W2L1,
            W2L2,
            W2L3,
            W2Boss,
            W3L1,
            W3L2,
            W3L3,
            W3Boss,
            W4L1,
            W4L2,
            W4L3,
            W4Boss,
            W5L1,
            W5L2,
            W5L3,
            W5Boss,
            W6L1,
            W6L2,
            W6L3,
            W6Boss,
            W7L1,
            W7L2,
            W7L3,
            W7Boss,
            W8L1,
            W8L2,
            W8L3,
            W8Boss,
            Frakin
        }

        public enum LevelNames_Kawaii
        {
            W1L1,
            W1L2,
            W1L3,
            W1Boss,
            W2L1,
            W2L2,
            W2L3,
            W2Boss,
            W3L1,
            W3L2,
            W3L3,
            W3Boss,
            W4L1,
            W4L2,
            W4L3,
            W4Boss,
            W5L1,
            W5L2,
            W5L3,
            W5Boss,
            W6L1,
            W6L2,
            W6L3,
            W6Boss,
            W7L1,
            W7L2,
            W7L3,
            W7Boss,
            W8L1,
            W8L2,
            W8L3,
            W8Boss,
            Frakin
        }
    }
}