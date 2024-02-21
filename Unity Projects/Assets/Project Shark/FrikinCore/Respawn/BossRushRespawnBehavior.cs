using System;
using FrikinCore.Enumeration;
using FrikinCore.Player;
using FrikinCore.Player.Weapons;
using FrikinCore.ScenesManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrikinCore.Respawn
{
    public class BossRushRespawnBehavior : BaseRespawn, IRespawn
    {
        public void PlayerDeath()
        {
            isPlayerDead = true;

            if (PlayerManager.instance.Player.Animator.GetBool(PlayerDataModel.IsDeadHash) == false)
            {
                PlayerManager.instance.Player.Animator.SetBool(PlayerDataModel.IsDeadHash, true);
            }
        }

        public void RespawnFunctionality()
        {
            GameManager.instance.scoringManagerCode.RemovePercentage(.1f);
            PlayerManager.instance.HealthController.SetHealthToMax();
            PlayerManager.instance.WeaponManagement.WeaponReload(AmmoHandler.ReloadAmmoAmount.ReloadAllFull);

            LoadToScene();
        }

        public void RespawnToLocation()
        {
            throw new NotImplementedException();
        }

        public void LoadToScene()
        {
            var timer = 0f;
            while (isPlayerDead)
            {
                timer += Time.deltaTime;
                if (timer >= 1.5f)
                {
                    isPlayerDead = false;
                    PlayerManager.instance.Player.PlayerMoveSpeed.DeactivateStatusEffect(GameEnums
                        .PlayerMovementEffects.Death);
                    Object.FindObjectOfType<SceneTransitionController>().LoadSpecificScene("Menu");
                }
            }
        }

        public void FindOnSceneChange()
        {

        }
    }
}