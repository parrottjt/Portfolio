using System;
using System.Collections.Generic;
using System.Linq;
using FrikinCore.Input;
using FrikinCore.Player.Items.NewWeapon;
using UnityEngine;

namespace FrikinCore.Player.Inventory
{
    public class WeaponInventory
    {
        int _weaponSlotLimit;
        List<InventorySlot<Weapon>> _weaponSlots;
        int _weaponIndex;

        public static event Action WeaponChange;
        public static event Action WeaponSlotChanged;

        public int SlotCount => _weaponSlotLimit;
        public int WeaponCount => _weaponSlots.Count(slot => slot.SlotEmpty == false);
        public Weapon WeaponSlot(int index) => index < _weaponSlotLimit ? _weaponSlots[index].Read() : null;
        public Weapon CurrentWeapon => WeaponSlot(_weaponIndex);
        
        public WeaponInventory(Weapon startingWeapon, int weaponSlotLimit)
        {
            _weaponSlotLimit = weaponSlotLimit;
            Add(startingWeapon);
        }

        #region Add,Remove,Change Weapon Slots

        public void Add(Weapon weapon)
        {
            if (_weaponSlots.Count >= _weaponSlotLimit) return;
            _weaponSlots.Add(new InventorySlot<Weapon>(weapon));
            WeaponSlotChanged?.Invoke();
        }
        
        public void Remove(Weapon weapon) => Remove(IndexOfWeapon(weapon));

        public void Remove(int index)
        {
            if (index == 0) return;
            ChangeSlotWeapon(index, null);
        }
        
        public void ChangeSlotWeapon(int index, Weapon weapon)
        {
            _weaponSlots[index].ChangeSlot(weapon);
            WeaponSlotChanged?.Invoke();
        }

        int IndexOfWeapon(Weapon weapon)
        {
            var weaponSlot = _weaponSlots.Find(slot => slot.Read() == weapon);
            var index = _weaponSlots.IndexOf(weaponSlot);
            return index;
        }
        
        #endregion

        #region Increase,Decrease,Swap WeaponIndex
        enum UpDown
        {
            Down = -1,
            Up = 1
        }

        public void WeaponScroll()
        {
            if (!PlayerManager.instance.PlayerActive) return;
            if (WeaponCount <= 1) return;

            if (InputManager.instance.WeaponIndexIncreaseInput()) Scroll(UpDown.Up);
            if (InputManager.instance.WeaponIndexDecreaseInput()) Scroll(UpDown.Down);
        }

        void Scroll(UpDown cycle) => UpdateWeaponIndex(CycleThroughWeaponArray(cycle));

        void UpdateWeaponIndex(int index)
        {
            _weaponIndex = Mathf.Clamp(index, 0, _weaponSlotLimit - 1);
            WeaponChange?.Invoke();
        }

        int CycleThroughWeaponArray(UpDown cycleThrough)
        {
            var index = _weaponIndex + (int)cycleThrough;

            if (index >= _weaponSlotLimit) index = 0;
            if (index < 0) index = _weaponSlotLimit - 1;

            return index;
        }
        
        #endregion
    }
}