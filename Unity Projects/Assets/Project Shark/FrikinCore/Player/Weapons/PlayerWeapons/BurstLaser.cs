using System;
using FrikinCore.Player.Items.NewWeapon;

namespace FrikinCore.Player.Weapons
{
    public class BurstLaser : Weapon
    {
        float burstTimer;
        int fireCount;

        public override WeaponName WeaponName => WeaponName.BurstLaser;
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
        //     //This will have to be updated
        //     if (fire)
        //     {
        //         burstTimer += Time.deltaTime;
        //         if (fireCount < 3 && burstTimer > .13f)
        //         {
        //             NormalFireMethod(spawnPositions, shadowShot);
        //             fireCount++;
        //             if (!canFireShadowShot)
        //             {
        //                 canFireShadowShot = ShadowShotCalculation();
        //             }
        //         }
        //     }
        //
        //     if (canFireShadowShot)
        //     {
        //         shadowShotTimer += Time.deltaTime;
        //         if (!(shadowShotTimer >= .15f)) return;
        //         NormalFireMethod(spawnPositions, true);
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
        //     PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        // }
        //
        // public void UpgradeFireMethod(Transform[] spawnPositions, bool shadowShot)
        // {
        //     throw new NotImplementedException();
        // }
        public BurstLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}