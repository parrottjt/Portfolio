﻿using FrikinCore.Player.Items.NewWeapon;

namespace FrikinCore.Player.Weapons
{
    public class BounceLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.BounceLazer;
        protected override void AttackFunctionality()
        {
            throw new System.NotImplementedException();
        }

        protected override void EquipFunctionality()
        {
            throw new System.NotImplementedException();
        }


        // public void ExecuteBasicFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
        //     out bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     shadowShot = false;
        //     if (fire)
        //     {
        //         NormalFireMethod(projectile, spawnPositions);
        //         shadowShot = ShadowShotCalculation();
        //     }
        // }
        //
        // public void ExecuteUpgradeFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
        //     out bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     shadowShot = false;
        //     if (CurrentWeaponLevel() == 1) return;
        //     if (fire)
        //     {
        //         UpgradeFireMethod(projectile, spawnPositions);
        //         shadowShot = ShadowShotCalculation();
        //     }
        // }
        //
        // public override void NormalFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     Handlers.FireProjectile(projectile, spawnPositions[0]);
        //
        //     DecreaseAmmo();
        //     ResetFireTimer();
        //
        //     PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        // }
        //
        // public override void UpgradeFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     Handlers.FireProjectile(projectile, spawnPositions[0]);
        //
        //     DecreaseAmmo(WeaponInfo.UpgradedAmmoUsageModifier);
        //     ResetFireTimer();
        //
        //     PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        // }
        public BounceLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}