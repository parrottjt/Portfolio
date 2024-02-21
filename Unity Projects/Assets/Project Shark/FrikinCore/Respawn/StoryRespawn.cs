using FrikinCore.CameraControl;
using FrikinCore.Loot.Teeth;
using FrikinCore.Player;
using FrikinCore.Player.Weapons;
using FrikinCore.ScenesManagement;
using UICore;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script may need to be split into a boss and level version

namespace FrikinCore.Respawn
{
    public class StoryRespawn : BaseRespawn, IRespawn
    {
        Vector2 checkPointPosition;

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
            PlayerManager.instance.Player.Animator.SetBool(PlayerDataModel.IsDeadHash, false);


            GameManager.instance.scoringManagerCode.RemovePercentage(.1f);
            PlayerManager.instance.HealthController.SetHealthToMax();
            PlayerManager.instance.WeaponManagement.WeaponReload(AmmoHandler.ReloadAmmoAmount.ReloadAllFull);


            if (!GameManager.GameSettings[Settings.IsBossScene] && hasHitRespawn)
            {
                RespawnToLocation();
                ToothPlacement.PlaceCorrectToothTypeWhereTileMapPlacementIs();
            }
            else
            {
                LoadToScene();
            }
        }

        public void RespawnToLocation()
        {
            //Change this back if it doesn't work, GameManager code
            Object.FindObjectOfType<CameraControlBase>().transform.position =
                new Vector3(checkPointPosition.x, checkPointPosition.y, -10);
            PlayerManager.instance.Player.transform.position =
                new Vector2(checkPointPosition.x, checkPointPosition.y);
            ToothPlacement.DeactivateCurrentLevelTeeth();

            if (UIManager.instance.InfoHolder.bossHealthBar.activeSelf)
            {
                UIManager.instance.InfoHolder.bossHealthBar.SetActive(false);
            }
        }

        public void LoadToScene()
        {
            SceneManager.LoadScene(SceneManagement.CurrentLevelName);
            PlayerRespawn();
        }

        public void FindOnSceneChange()
        {
            if (GameManager.GameStatesDictionary[GameStates.Menu]) return;
            checkPointPosition = PlayerManager.instance.Player.transform.position;
        }

        public void SetRespawnAndSpringLocation(Vector2 loc)
        {
            hasHitRespawn = true;
            checkPointPosition = loc;
        }
    }
}