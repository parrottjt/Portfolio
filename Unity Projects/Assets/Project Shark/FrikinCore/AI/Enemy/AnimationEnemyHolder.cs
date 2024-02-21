using UnityEngine;

namespace FrikinCore.AI.Enemy
{
	public class AnimationEnemyHolder : MonoBehaviour
	{
		DataEnemy dataEnemy;

		void OnEnable() => dataEnemy = GetComponent<DataEnemy>();

		readonly int AnimatorFire = Animator.StringToHash("Fire");
		public void SetAnimationFireTrue() => dataEnemy.Animator.SetBool(AnimatorFire, true);
		public void SetAnimationFireFalse() => dataEnemy.Animator.SetBool(AnimatorFire, false);

		readonly int AnimatorInCombat = Animator.StringToHash("InCombat");
		public void SetAnimationInCombatTrue() => dataEnemy.Animator.SetBool(AnimatorInCombat, true);
		public void SetAnimationInCombatFalse() => dataEnemy.Animator.SetBool(AnimatorInCombat, false);
	}
}
