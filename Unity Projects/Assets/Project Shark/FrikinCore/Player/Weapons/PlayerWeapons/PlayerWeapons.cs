using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrikinCore.Player.Weapons
{
    [CreateAssetMenu]

    public class PlayerWeapons : ScriptableObject
    {
        public enum FireFunctionType
        {
            Click,
            Charge,
            Hold
        }

        [Serializable]
        public struct UISprites
        {
            public Sprite uiWeaponOutline;
            public Sprite uiAmmoSprite;
            public Color uiBackgroundColor;
        }

        [SerializeField] WeaponName weaponName;
        [SerializeField] string weaponNameForUI;

        [FormerlySerializedAs("nameColor")] [ColorPalette] [SerializeField]
        Color weaponColor;

        [SerializeField] FireFunctionType normalFireType;
        [SerializeField] FireFunctionType upgradeFireType;
        [SerializeField] int normalMaxAmmo = 10;
        [SerializeField] float upgradedAmmoUsageModifier = 1f;
        [SerializeField] float fireTime = 0.5f, chargeDecayModifier = 1;
        [SerializeField] float chargeTime = 2f;
        [SerializeField] float reloadTime = 1f;
        [SerializeField] GameObject normalProj;
        [SerializeField] GameObject upgradeProj;
        [SerializeField] GameObject normalShadowProjectile;
        [SerializeField] GameObject upgradedShadowProjectile;

        [FormerlySerializedAs("weaponWheelSprite")] [SerializeField]
        Sprite weaponGameSprite;

        [SerializeField] UISprites uiSprites;

        [FormerlySerializedAs("weaponAniamtion")] [SerializeField]
        AnimationClip weaponAnimation;

        [SerializeField] bool useWholeNumberOnSlider;

        public WeaponName WeaponName => weaponName;
        public string WeaponNameForUI => weaponNameForUI;
        public Color WeaponColor => weaponColor;
        public float FireTime => fireTime;
        public float ChargeTime => chargeTime;
        public float ReloadTime => reloadTime;

        public float ChargeDecayModifier => chargeDecayModifier;

        public Sprite WeaponGameSprite => weaponGameSprite;
        //public Sprite WeaponGameSprite;
        public AnimationClip WeaponAnimation => weaponAnimation;
        public bool UseWholeNumberOnSlider => useWholeNumberOnSlider;
        public FireFunctionType NormalFireType => normalFireType;
        public FireFunctionType UpgradeFireType => upgradeFireType;
        public UISprites SpritesForUI => uiSprites;
        public int NormalMaxAmmo => normalMaxAmmo;
        public float UpgradedAmmoUsageModifier => upgradedAmmoUsageModifier;
        public GameObject NormalProjectile => normalProj;
        public GameObject UpgradeProjectile => upgradeProj;
        public GameObject NormalShadowProjectile => normalShadowProjectile;
        public GameObject UpgradedShadowProjectile => upgradedShadowProjectile;
    }
}
