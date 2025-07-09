using System;
using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Utils;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class BakaCannon : Weapon
    {
        public override WeaponName WeaponName => WeaponName.BakaCannon;
        protected override void AttackFunctionality()
        {
            throw new NotImplementedException();
        }

        protected override void EquipFunctionality()
        {
            throw new NotImplementedException();
        }

        public void ExecuteBasicFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
            out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
        }

        public void ExecuteUpgradeFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
            out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
        }

        public void NormalFireMethod(GameObject projectile, Transform[] spawnPositions)
        {
            Handlers.FireProjectile(projectile, spawnPositions);

            //DecreaseAmmo();
            //ResetFireTimer();

            //PlaySoundEffect(SoundManager.instance.ChargeLaserSfx);
        }

        // public void ExecuteBasicFunctionality(bool fire, Transform[] spawnPositions, bool shadowShot,
        //     LineRenderer lineRenderer = null)
        // {
        //     if (fire && !hasFired)
        //     {
        //         if (chargeTimer < weaponInfo.FireTime)
        //         {
        //             chargeTimer += Time.deltaTime;
        //         }
        //
        //         hasCharge = true;
        //     }
        //
        //     if (!fire && hasCharge && !hasFired && chargeTimer >= weaponInfo.FireTime)
        //     {
        //         NormalFireMethod(spawnPositions, shadowShot);
        //         canFireShadowShot = ShadowShotCalculation();
        //         hasFired = true;
        //     }
        //
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
        public BakaCannon(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}