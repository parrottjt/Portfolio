using System;
using FrikinCore.Player.Items.NewWeapon;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class TaserLaser : Weapon
    {
        public override WeaponName WeaponName => WeaponName.TaserLazer;
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
        //     lineRenderer.enabled = fire;
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
        //     throw new NotImplementedException();
        // }
        //
        // public void UpgradeFireMethod(Transform[] spawnPositions, bool shadowShot)
        // {
        //     throw new NotImplementedException();
        // }
        public TaserLaser(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
        }
    }
}