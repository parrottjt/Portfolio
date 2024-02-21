using System.Collections.Generic;
using System.Linq;
using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Stats;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class AmmoHandler
    {
        public enum ReloadAmmoAmount
        {
            ReloadAllFull,
            ReloadAllHalf
        }

        public struct Ammo
        {
            public int ammo;
            public int passiveReloadTick;

            public Ammo(int ammo)
            {
                this.ammo = ammo;
                passiveReloadTick = 0;
            }
        }

        PlayerWeaponManagement _management;
        readonly Dictionary<Weapon, Ammo> _currentWeaponAmmoList = new ();

        float _passiveReloadDelay;
        float _passiveReloadModifier;

        int _passiveReloadDelayTicks;

        int PassiveReloadDelayMaxTick => Mathf.RoundToInt(_passiveReloadDelay * _passiveReloadModifier
                                                          / TickTimeTimer.TickTimerMax);

        public bool PlayerHasWeapon(Weapon weaponName) => _currentWeaponAmmoList.ContainsKey(weaponName);

        public AmmoHandler(float passiveReloadDelay, float passiveReloadModifier)
        {
            _passiveReloadDelay = passiveReloadDelay;
            _passiveReloadModifier = passiveReloadModifier;
        }

        public void ClearWeaponAmmo()
        {
            _currentWeaponAmmoList.Clear();
        }

        public void AddWeaponAmmo(Weapon weapon)
        {
            if (PlayerHasWeapon(weapon)) return;
            _currentWeaponAmmoList.Add(weapon, new Ammo(weapon.WeaponInfo.MaxAmmo));
        }

        public void RemoveWeaponAmmo(Weapon weapon)
        {
            if (PlayerHasWeapon(weapon) == false) return;
            _currentWeaponAmmoList.Remove(weapon);
        }

        public int CurrentAmmoAmountOfWeapon(Weapon weapon) => GetWeaponAmmo(weapon);

        public int MaxAmmoAmountOfWeapon(Weapon weapon) => (int)UpdatedStatManager.GetStat(
            GameEnums.PermanentStats.MaxAmmoAdjust).GetStatValue(weapon.WeaponInfo.MaxAmmo);

        public bool AllWeaponsFull() =>
            _currentWeaponAmmoList.Keys.ToList().TrueForAll(
                weaponName => _currentWeaponAmmoList[weaponName].ammo == MaxAmmoAmountOfWeapon(weaponName));

        public bool IsAmmoTypeFull(Weapon weapon) =>
            CurrentAmmoAmountOfWeapon(weapon) == MaxAmmoAmountOfWeapon(weapon);

        bool ReloadDelay() => _passiveReloadDelayTicks >= PassiveReloadDelayMaxTick;

        bool PlayerHasFiredWeaponRecently()
        {
            if (!InputManager.instance.AttackInput()) return false;
            _passiveReloadDelayTicks = 0;
            return true;
        }

        int GetWeaponAmmo(Weapon weaponName)
        {
            if (PlayerHasWeapon(weaponName) == false) return -1;
            return _currentWeaponAmmoList[weaponName].ammo;
        }

        public void DecreaseAmmo(Weapon weapon, int amount) => ChangeAmmo(weapon, -Mathf.Abs(amount));

        public void IncreaseAmmo(Weapon weapon, int amount) => ChangeAmmo(weapon, Mathf.Abs(amount));

        public void Reload(Weapon weapon, ReloadAmmoAmount reloadAmmoAmount)
        {
            switch (reloadAmmoAmount)
            {
                case ReloadAmmoAmount.ReloadAllFull:
                    foreach (var playerWeapon in _currentWeaponAmmoList.Keys)
                    {
                        IncreaseAmmo(playerWeapon, playerWeapon.WeaponInfo.MaxAmmo);
                    }

                    break;
                case ReloadAmmoAmount.ReloadAllHalf:
                    foreach (var playerWeapon in _currentWeaponAmmoList.Keys)
                    {
                        var amount = playerWeapon.WeaponInfo.MaxAmmo / 2;
                        if (playerWeapon != weapon)
                            IncreaseAmmo(playerWeapon,
                                playerWeapon.WeaponInfo.MaxAmmo % 2 == 1 ? amount + 1 : amount);
                    }

                    IncreaseAmmo(weapon, weapon.WeaponInfo.MaxAmmo);
                    break;
            }
        }

        void ChangeAmmo(Weapon weapon, int amount)
        {
            if (PlayerHasWeapon(weapon) == false) return;
            var currentWeaponAmmo = _currentWeaponAmmoList[weapon];
            currentWeaponAmmo.ammo =
                Mathf.Clamp(currentWeaponAmmo.ammo + amount, 0, MaxAmmoAmountOfWeapon(weapon));
        }

        void PassiveReload(Weapon weapon)
        {
            var currentWeaponAmmo = _currentWeaponAmmoList[weapon];
            currentWeaponAmmo.passiveReloadTick += 1;
            var reloadTime = UpdatedStatManager.GetStat(GameEnums.PermanentStats.ReloadCooldownAdjust)
                .GetStatValue(weapon.WeaponInfo.ReloadTime);
            if (currentWeaponAmmo.passiveReloadTick > reloadTime)
            {
                currentWeaponAmmo.passiveReloadTick = 0;
                IncreaseAmmo(weapon, 1);
            }
        }

        public void OnTick()
        {
            if (ReloadDelay() == false) _passiveReloadDelayTicks += 1;
            if (AllWeaponsFull()) return;
            foreach (var playerWeapon in _currentWeaponAmmoList.Keys)
            {
                if (IsAmmoTypeFull(playerWeapon)) continue;
                if (PlayerHasFiredWeaponRecently() == false && ReloadDelay())
                    PassiveReload(playerWeapon);
                else PassiveReload(playerWeapon);
            }
        }
    }
}
