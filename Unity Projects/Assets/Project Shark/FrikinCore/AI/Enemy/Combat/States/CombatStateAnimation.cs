
using FrikinCore.Interfaces;

namespace FrikinCore.AI.Enemy.Combat
{
    public class CombatStateAnimation : ICombatState
    {
        readonly DataEnemy dataEnemy;

        public CombatStateAnimation(DataEnemy dataEnemy)
        {
            this.dataEnemy = dataEnemy;
        }

        public void EnterState()
        {
        }

        public void ExecuteState()
        {
        }

        public void ExitState()
        {
        }

        public void Attack()
        {
        }

        public void EnterCombat()
        {
            dataEnemy.AnimationEventHolder.SetAnimationInCombatTrue();
        }

        public void ExecuteCombat()
        {
        }

        public void StopCombat()
        {
            dataEnemy.AnimationEventHolder.SetAnimationInCombatFalse();
        }
    }
}