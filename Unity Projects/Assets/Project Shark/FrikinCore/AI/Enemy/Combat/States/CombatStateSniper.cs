using FrikinCore.AI.Enemy.Combat.AttackHandlers;
using FrikinCore.Interfaces;

namespace FrikinCore.AI.Enemy.Combat
{
    public class CombatStateSniper : ICombatState
    {
        readonly RangedWeaponAttack _rangedWeaponAttack;

        public CombatStateSniper(DataEnemy dataEnemy)
        {
            _rangedWeaponAttack = dataEnemy.weaponAttack as RangedWeaponAttack;
        }

        public void Attack()
        {
            _rangedWeaponAttack.Attack();
        }

        public void EnterCombat()
        {

        }

        public void ExecuteCombat()
        {
            Attack();
        }

        public void StopCombat()
        {

        }
    }
}
