using FrikinCore.Loot;
using FrikinCore.Player.Managers;
using UICore;

namespace FrikinCore.Collectable
{
    public class CollectableHeartPickup : Collectable
    {
        int heartValue;

        protected override void OnPickUp()
        {
            SetEnableOnComponentsTo(false);
            SetHeartCollectedInBitArray();
        }

        void SetHeartCollectedInBitArray()
        {
            heartValue = GetComponentInParent<HeartPieceController>().InTutorial
                ? GetComponentInParent<HeartPieceController>().TutorialHeartPieceNumber
                : GameManager.instance.storySceneInt;

            if (GameManager.instance.storySceneInt > 1)
            {
                PlayerDataManager.instance.heartCollectable_Levels.Set(heartValue - 2, true);
            }
            else
            {
                PlayerDataManager.instance.heartCollectables_FrikinsGym.Set(heartValue, true);
            }

            PlayHeartCollectedAnimation();
        }

        void PlayHeartCollectedAnimation()
        {
            UIManager.instance.UiPlayerHudController.HeartPiece.IncreaseHeartPieces();
        }
    }
}
