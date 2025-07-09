using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Utils;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class NormalLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.NormalLazer;

        protected override void AttackFunctionality()
        {
            throw new System.NotImplementedException();
        }

        protected override void EquipFunctionality()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteBasicFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
            out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
            if (fire)
            {
                NormalFireMethod(projectile, spawnPositions);
                //shadowShot = ShadowShotCalculation();
            }
        }

        public void ExecuteUpgradeFunctionality(bool fire, GameObject projectile, Transform[] spawnPositions,
            out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
            //if (CurrentWeaponLevel() == 1) return;
            if (fire)
            {
                //UpgradeFireMethod(projectile, spawnPositions);
                //shadowShot = ShadowShotCalculation();
            }
        }

        public void NormalFireMethod(GameObject projectile, Transform[] spawnPositions)
        {
            Handlers.FireProjectile(projectile, spawnPositions[0]);

            //ResetFireTimer();

            //PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        }

        public void UpgradeFireMethod(GameObject projectile, Transform[] spawnPositions)
        {
            Handlers.FireProjectile(projectile, spawnPositions[0]);

            //ResetFireTimer();

            //PlaySoundEffect(SoundManager.instance.RedLaserSfx);
        }

        public NormalLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}