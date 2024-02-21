using FrikinCore.Player.Weapons;
using UnityEngine;

namespace FrikinCore.Player.Items.NewWeapon
{
    public enum WeaponType
    {
        Ranged,
        Melee
    }
    
    [CreateAssetMenu]
    public class NewWeaponInfo : ItemInfo
    {
        [SerializeField] WeaponName _weaponName;
        public WeaponName WeaponName => _weaponName;
        public WeaponType WeaponType { get; }
        public Sprite GameSprite { get; }
        public PlayerWeapons.UISprites UISprites { get; }
        public PlayerWeapons.FireFunctionType AttackType { get; }
        public int MaxAmmo { get; }
        public float ReloadTime { get; }
        public bool UseWholeNumberOnSlider { get; }
        public Color WeaponColor { get; }
        public string WeaponNameForUI { get; }
    }
}
