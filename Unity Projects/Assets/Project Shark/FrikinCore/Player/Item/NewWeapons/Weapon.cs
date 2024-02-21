using FrikinCore.Player.Weapons;

namespace FrikinCore.Player.Items.NewWeapon
{
    public abstract class Weapon : Item<Weapon>, IWeapon
    {
        protected Weapon(NewWeaponInfo weaponInfo) : base(weaponInfo)
        {
            WeaponInfo = weaponInfo;
        }

        public void Attack() => AttackFunctionality();

        public override Weapon Get() => this;

        public NewWeaponInfo WeaponInfo { get; }

        public void EquipWeapon() => EquipFunctionality();

        public void ExecuteFunctionality()
        {
            AttackFunctionality();
        }

        public abstract WeaponName WeaponName { get; }
        protected abstract void AttackFunctionality();
        protected abstract void EquipFunctionality();
    }
}