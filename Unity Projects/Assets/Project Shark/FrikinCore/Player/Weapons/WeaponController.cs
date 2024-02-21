using UnityEngine;

namespace FrikinCore.Player.Items.NewWeapon
{
    public interface IWeapon
    {
        void EquipWeapon();
        void ExecuteFunctionality();
    }

    public class WeaponController
    {
        IWeapon currentWeapon { get; set; }
        public Weapon CurrentWeapon => currentWeapon as Weapon;

        public Transform[] projectileSpawns;

        NewWeaponInfo CurrentWeaponInformation => CurrentWeapon.WeaponInfo;

        public void ChangeWeapon(IWeapon newWeapon)
        {
            currentWeapon = newWeapon;
            currentWeapon.EquipWeapon();
        }

        public void ExecuteFunctionality(bool fire, GameObject projectile, out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            ExecuteNormalFunctionality(fire, projectile, out shadowShot, lineRenderer);
        }

        void ExecuteNormalFunctionality(bool fire, GameObject projectile, out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
            currentWeapon?.ExecuteFunctionality();
        }

        void ExecuteUpgradedFunctionality(bool fire, GameObject projectile, out bool shadowShot,
            LineRenderer lineRenderer = null)
        {
            shadowShot = false;
            //currentWeapon?.ExecuteUpgradeFunctionality(fire, projectile, projectileSpawns, out shadowShot, lineRenderer);
        }
    }
}