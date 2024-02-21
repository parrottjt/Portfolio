using System.Collections;
using UnityEngine;

namespace FrikinCore.Player.Managers
{
    public partial

    class PlayerDataManager
    {

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject Holder;

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject Replacement;

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject frikinGymHeart1Holder;

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject frikinGymHeart2Holder;

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject frikinGymHeart3Holder;

        [Header("Heart Pick Up Bool Testing,will be [HideInInspector] later")] [HideInInspector] [SerializeField]
        private GameObject frikinGymHeart4Holder;

        public enum HeartCollectables_Levels
        {
            W1L1,
            W1L2,
            W1L3,
            W1Boss, //No Hearpiece In This Level
            W2L1,
            W2L2,
            W2L3,
            W2Boss, //No Hearpiece In This Level
            W3L1,
            W3L2,
            W3L3,
            W3Boss, //No Hearpiece In This Level
            W4L1,
            W4L2,
            W4L3,
            W4Boss, //No Hearpiece In This Level
            W5L1,
            W5L2,
            W5L3,
            W5Boss, //No Hearpiece In This Level
            W6L1,
            W6L2,
            W6L3,
            W6Boss, //No Hearpiece In This Level
            W7L1,
            W7L2,
            W7L3,
            W7Boss, //No Hearpiece In This Level
            W8L1,
            W8L2,
            W8L3,
            W8Boss, //No Hearpiece In This Level
            Frakin
        }

        public enum HeartCollectables_FrikinsGym
        {
            FG1,
            FG2,
            FG3,
            FG4
        }

        public BitArray heartCollectable_Levels;
        public BitArray heartCollectables_FrikinsGym;

    }
}