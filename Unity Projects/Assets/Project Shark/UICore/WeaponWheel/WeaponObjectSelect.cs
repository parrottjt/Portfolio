using FrikinCore.Player.Weapons;
using UnityEngine;

namespace UICore.WeaponWheel
{
    public class WeaponObjectSelect : MonoBehaviour
    {
        public PlayerWeapons newWeapon;
        public GameObject test;

        void Update()
        {
            test = gameObject;
        }

        public void UpdateWeapon(PlayerWeapons weapon)
        {
            newWeapon = weapon;
        }
    }
}
