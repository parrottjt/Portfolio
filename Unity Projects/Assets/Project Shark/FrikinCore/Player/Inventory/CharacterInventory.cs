using System.Collections.Generic;
using FrikinCore.Player.Items.Equipment;
using FrikinCore.Player.Items.NewWeapon;

namespace FrikinCore.Player.Inventory
{
    public class CharacterInventory
    {
        /// <summary>
        /// 2 Weapon Slots
        /// 1 Equipment Slot
        ///
        /// Store Items
        /// </summary>
        public readonly WeaponInventory Weapon;
        List<InventorySlot<Equipment>> _equipmentSlots = new ();

        public int WeaponCount => Weapon.WeaponCount;
        
        public CharacterInventory(Weapon defaultWeapon)
        {
            //todo: When weapon limit is set we need to unhard code this
            Weapon = new WeaponInventory(defaultWeapon, 2);
        }
    }
}