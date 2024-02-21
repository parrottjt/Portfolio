using System;
using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Sfx;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class ChargeLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.ChargeLazer;
        protected override void AttackFunctionality()
        {
            throw new NotImplementedException();
        }

        protected override void EquipFunctionality()
        {
            throw new NotImplementedException();
        }

        // public void ExecuteBasicFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
        //     out bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     shadowShot = false;
        // }
        //
        // public void ExecuteUpgradeFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
        //     out bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     shadowShot = false;
        // }
        //
        // public override void NormalFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public override void UpgradeFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public void ExecuteBasicFunctionality(bool fire, Transform[] spawnPositions, bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     //Convert To Switch
        //     //Charge
        //     if (fire && !hasFired)
        //     {
        //         if (chargeTimer < 2f)
        //         {
        //             chargeTimer += Time.deltaTime;
        //         }
        //
        //         hasCharge = true;
        //     }
        //
        //     //Fire
        //     if (!fire && hasCharge && !hasFired)
        //     {
        //         NormalFireMethod(spawnPositions, shadowShot);
        //
        //         canFireShadowShot = ShadowShotCalculation();
        //         hasFired = true;
        //     }
        //
        //     //Recharge
        //     if (hasFired)
        //     {
        //         if (chargeTimer > 0)
        //         {
        //             chargeTimer -= Time.deltaTime *
        //                            (1 / PlayerManager.instance.WeaponManagement.CurrentWeapon.ChargeDecayModifier);
        //             hasCharge = false;
        //         }
        //
        //         if (chargeTimer <= 0)
        //         {
        //             chargeTimer = 0;
        //             hasFired = false;
        //         }
        //     }
        //
        //     if (canFireShadowShot)
        //     {
        //         shadowShotTimer += Time.deltaTime;
        //         if (shadowShotTimer < .15f) return;
        //         NormalFireMethod(spawnPositions, shadowShot);
        //         shadowShotTimer = 0;
        //         canFireShadowShot = false;
        //     }
        // }
        //
        // public void ExecuteUpgradeFunctionality(bool fire, Transform[] spawnPositions, bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public void NormalFireMethod(Transform[] spawnPositions, bool shadowShot)
        // {
        //     ResetFireTimer();
        //     DecreaseAmmo();
        //
        //     PlaySoundEffect(SoundManager.instance.ChargeLaserSfx);
        // }
        //
        // public void UpgradeFireMethod(Transform[] spawnPositions, bool shadowShot)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // int ChargeLaserProjectileIndexSelector()
        // {
        //     if (chargeTimer < .75f) return 0;
        //     if (chargeTimer >= .75f && chargeTimer < 1.5f) return 1;
        //     if (chargeTimer >= 1.5f && chargeTimer < 2f) return 2;
        //     return 3;
        // }
        public ChargeLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}