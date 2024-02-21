using FrikinCore.Enumeration;
using FrikinCore.Player;

namespace FrikinCore.Respawn
{
	public class BaseRespawn
	{
		public bool hasHitRespawn;
		protected bool isPlayerDead;

		public bool GetIsPlayerDead() => isPlayerDead;

		public void PlayerRespawn()
		{
			isPlayerDead = false;
			PlayerManager.instance.Player.GracePeriod.ActivateRespawnEffectOnPlayer();
			PlayerManager.instance.Player.PlayerMoveSpeed.DeactivateStatusEffect(
				GameEnums.PlayerMovementEffects.Death);
		}
	}
}
