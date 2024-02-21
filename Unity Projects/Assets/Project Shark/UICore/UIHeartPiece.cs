using FrikinCore.Player;
using FrikinCore.Save;
using UnityEngine;

namespace UICore
{
    public class UIHeartPiece : MonoBehaviour
    {
        [SerializeField] Animator heartPieceAnimator;

        int heartPieces;
        static readonly int StartAnimation = Animator.StringToHash("StartAnimation");
        static readonly int PieceCount = Animator.StringToHash("PieceCount");

        public void IncreaseHeartPieces()
        {
            heartPieces++;
            PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryHeartPieces] = heartPieces;
            DisplayHeartPieces();
            if (heartPieces == 4)
            {
                ResetHeartPieces();
                PlayerManager.instance.HealthController.AddMaxHealth(2);
            }
        }

        void DisplayHeartPieces()
        {
            heartPieceAnimator.SetTrigger(StartAnimation);
            heartPieceAnimator.SetInteger(PieceCount, heartPieces);
        }

        void ResetHeartPieces()
        {
            heartPieces = 0;
            PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryHeartPieces] = heartPieces;
        }
    }
}
