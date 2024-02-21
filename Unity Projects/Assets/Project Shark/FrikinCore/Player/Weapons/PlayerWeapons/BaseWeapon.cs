using System.Collections.Generic;
using System.Linq;
using FrikinCore.DevelopmentTools;
using FrikinCore.Sfx;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public abstract class BaseWeapon
    {
        //Base for all weapons
        protected PlayerWeapons weaponInfo;
        protected bool canFireShadowShot;
        protected float shadowShotTimer;

        protected bool hasCharge, hasFired;
        protected float chargeTimer;

        bool weaponModeSetToUpgrade;

        public bool WeaponModeSetToUpgrade
        {
            get => weaponModeSetToUpgrade;
            set => weaponModeSetToUpgrade = CurrentWeaponLevel() > 1 && value;
        }

        public enum WeaponLevel
        {
            Basic,
            Upgraded,
            FullyUpgraded
        }

        Dictionary<WeaponLevel, bool> weaponLevelDictionary = new Dictionary<WeaponLevel, bool>
        {
            { WeaponLevel.Basic, true },
            { WeaponLevel.Upgraded, false },
            { WeaponLevel.FullyUpgraded, false }
        };

        public PlayerWeapons WeaponInfo => weaponInfo;

        public int CurrentWeaponLevel() =>
            weaponLevelDictionary.Keys.Count(weaponLevel => weaponLevelDictionary[weaponLevel]);

        public float GetChargeTimer() => chargeTimer;

        public void UnlockWeaponLevel(WeaponLevel weaponLevel)
        {
            if (weaponLevel == WeaponLevel.Basic)
            {
                DebugScript.LogYellowText(this, "Basic Weapon has already been unlocked");
                return;
            }

            if (weaponLevel == WeaponLevel.FullyUpgraded && weaponLevelDictionary[WeaponLevel.Upgraded] == false)
            {
                DebugScript.LogYellowText(this, "Full Grade tried to be assessed without normal upgrade");
                return;
            }

            weaponLevelDictionary[weaponLevel] = true;
        }

        protected bool ShadowShotCalculation()
        {
            if (PlayerManager.instance.Player.ShadowShotAdjust > 0)
            {
                return Random.Range(0f, 100.00f) <= PlayerManager.instance.Player.ShadowShotAdjust * 100;
            }

            return false;
        }

        public virtual WeaponName WeaponName => WeaponName.NormalLazer;

        protected void PlaySoundEffect(SoundClip clipToPlay)
        {
            if (PlayerManager.instance.WeaponManagement.CurrentWeaponAmmo > 0)
            {
                SoundManager.instance.RandomizeSfx(!DevTools.devToolsDictionary[DevTool.KawaiiMode]
                    ? clipToPlay
                    : SoundManager.instance.KawaiiFunSfx);
            }
        }

        protected void DecreaseAmmo(float upgradeAmmoModifier = 1)
        {
            int amount = 1;
            PlayerManager.instance.WeaponManagement.DecreaseCurrentWeaponAmmo((int)(amount * upgradeAmmoModifier));
        }

        protected void ResetFireTimer() => PlayerManager.instance.Player.FiringController.ResetFireTimeToZero();
        public abstract void NormalFireMethod(GameObject projectile, Transform[] spawnPositions);
        public abstract void UpgradeFireMethod(GameObject projectile, Transform[] spawnPositions);
    }
}

