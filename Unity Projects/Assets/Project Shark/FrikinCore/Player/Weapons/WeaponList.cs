using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrikinCore.Player.Items.NewWeapon;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public enum WeaponName
    {
        NormalLazer,
        ChargeLazer,
        TrackingLazer,
        SplitLazer,
        TaserLazer,
        RapidFireLazer,
        BounceLazer,
        ShotgunLazer,
        BakaCannon,
        TeethGun,

        //Not Story
        BurstLaser,
        FlameThrower,
        PeaShooter,
        SharkRepellent,
    }

    public class WeaponList : MonoBehaviour
    {
        public static readonly Dictionary<int, Weapon> WeaponDictionary = new ();

        static bool _isInstanced;

        public void Awake()
        {
            Instance();
        }

        void Instance()
        {
            if (_isInstanced) return;
            var weaponClass = Assembly.GetAssembly(typeof(Weapon)).GetTypes()
                .Where(t => typeof(Weapon).IsAssignableFrom(t) && t.IsAbstract == false).ToList();

            foreach (var weaponType in weaponClass)
            {
                var weaponInfo = Resources.Load($"Spawnable/{weaponType.Name}") as NewWeaponInfo;
                
                if (weaponInfo == null) continue;
                if (WeaponDictionary.ContainsKey(weaponInfo.ID)) continue;
                var weaponFunction = (Weapon)Activator.CreateInstance(weaponType, args:weaponInfo);
                WeaponDictionary.Add(weaponInfo.ID, weaponFunction);
            }

            Debug();
            
            _isInstanced = true;
        }

        void Debug()
        {
            foreach (var keyValuePair in WeaponDictionary)
            {
                print($"ID: {keyValuePair.Key} | Value: {keyValuePair.Value}");
            }
        }
    }
}