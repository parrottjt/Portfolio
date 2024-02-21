using System;
using FrikinCore.Player.Items.NewWeapon;

namespace FrikinCore.Player.Weapons
{
    public class SplitLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.SplitLazer;
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
        //     if (fire)
        //     {
        //         NormalFireMethod(spawnPositions, shadowShot);
        //         if (!canFireShadowShot)
        //         {
        //             canFireShadowShot = ShadowShotCalculation();
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
        //     var spawns = spawnPositions[6].GetComponentsInChildren<Transform>();
        //
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
        public SplitLaser(NewWeaponInfo weaponInfo) : base(weaponInfo) { }
    }
}