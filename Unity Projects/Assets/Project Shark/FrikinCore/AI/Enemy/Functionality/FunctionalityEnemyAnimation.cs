using FrikinCore.AI.Combat;
using FrikinCore.AI.Enemy.Combat;

namespace FrikinCore.AI.Enemy.Functionality
{
    public class FunctionalityEnemyAnimation : FunctionalityEnemyAbstract
    {
        protected override void Start()
        {
            base.Start();
            combatState = new CombatStateAnimation(dataEnemy);

            combatStateMachine = new CombatStateMachine(combatState, gameObject);
            dataEnemy.Health.DeclarePawnDeathFunctionality(() =>
            {
                combatStateMachine.StopCombat();
                combatStateMachine.RunCombat();
            });
        }

        void Update()
        {
            combatStateMachine.RunCombat();
        }

        public override void OnPause()
        {
            base.OnPause();
            dataEnemy.Animator.speed = 0; //This might be moved into the normal behavior as everything will have 
        }

        public override void OnUnpause()
        {
            base.OnUnpause();
            dataEnemy.Animator.speed = 1;
        }
    }
}