using FrikinCore.Player.Items.NewWeapon;

namespace FrikinCore.Player.Weapons
{
    public class RapidFireLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.RapidFireLazer;
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
        //     if (CurrentWeaponLevel() == 1)
        //     {
        //         shadowShot = false;
        //         return;
        //     }
        //
        //     shadowShot = fire && ShadowShotCalculation();
        //     if (lineRenderer != null) lineRenderer.enabled = fire;
        // }
        //
        // public override void NormalFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     Handlers.FireProjectile(projectile, spawnPositions[5]);
        //
        //     DecreaseAmmo();
        //     ResetFireTimer();
        //
        //     PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        // }
        //
        // public override void UpgradeFireMethod(GameObject projectile, Transform[] spawnPositions)
        // {
        //     DecreaseAmmo(WeaponInfo.UpgradedAmmoUsageModifier);
        // }
        public RapidFireLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}