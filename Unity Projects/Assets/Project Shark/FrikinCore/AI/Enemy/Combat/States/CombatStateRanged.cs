using FrikinCore.Interfaces;

namespace FrikinCore.AI.Enemy.Combat
{
	public class CombatStateRanged : ICombatState
	{
		readonly DataEnemy dataEnemy;

		public CombatStateRanged(DataEnemy dataEnemy)
		{
			this.dataEnemy = dataEnemy;
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

		public void Attack()
		{
			dataEnemy.weaponAttack.Attack(dataEnemy.AnimationEventHolder.SetAnimationFireTrue);
		}
	}
}
