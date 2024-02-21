using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class InfoHolder_IGM : MonoBehaviour
    {
        [Header("For Textbox Manager")] public GameObject textPanel;
        public Text textBox1, textBox2;
        public Image frikinImage, frakinImage;

        [Header("Game UI")] public GameObject canvas;
        public GameObject backgroundManager;
        public GameObject imageForFadeInOut;

        [Header("Boss Health Bar")] public GameObject bossHealthBar;
        public Image bossFill, bossBackground;
        public TMP_Text bossText, sharkFamilyText;
        public GameObject sharkFamilyHealthBars;
        public SharkFamilyHealthBarInfo[] sharkFamilyHealthBarInfos;

        [Header("Top Left Corner")] public Image upRightBackgroundImage;
        public Image teethImage;
        public Text teethText;
        public Image[] heartImages;
        public Slider healthBar;

        [Header("Bottom Left Corner")] public Text laserName;
        public Slider ammoSlider;
        public Image laserFill, laserBackground, laserNameBackground, ammoBorderBackground;

        [Header("Weapon Wheel")] public GameObject weaponWheel;
        public GameObject normalSlot;



        [Header("GameObjects for Other Scripts")]
        public GameObject backGroundManager;

        public GameObject bottomLeftCorner,
            upperLeftCorner;

        public GameObject rewardPanel, teethEarnedPanel;
        public Image[] rewardPanelBackgrounds;

        [Serializable]
        public struct UpgradeVariables
        {
            public Slider statSlider;
            public GameObject textForSlider;
        }

        [Serializable]
        public struct SharkFamilyHealthBarInfo
        {
            public Slider memberSlider;
            public GameObject memberBackground, memberFill;
        }
    }
}