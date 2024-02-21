using FrikinCore.Player.Managers;
using UnityEngine;

namespace FrikinCore.Loot
{
    public class HeartPieceController : MonoBehaviour
    {
        [SerializeField] GameObject replacementChest, heartChest;

        [SerializeField] bool inTutorial;

        [DrawIf("inTutorial", true)] [Range(0, 3)] [Tooltip("This value must be 0,1,2,3")] [SerializeField]
        int frikinsGymHeartPieceNumber;

        public bool InTutorial => inTutorial;

        public int TutorialHeartPieceNumber => frikinsGymHeartPieceNumber;

        void Start()
        {
            CheckWhichChestToShow();
            GameManager.CallOnSceneChange += CheckWhichChestToShow;
        }

        void OnDestroy()
        {
            GameManager.CallOnSceneChange -= CheckWhichChestToShow;
        }

        void CheckWhichChestToShow()
        {
            if (GameManager.instance.storySceneInt > 2)
            {
                heartChest.SetActive(
                    !PlayerDataManager.instance.heartCollectable_Levels.Get(GameManager.instance.storySceneInt - 2));
                replacementChest.SetActive(
                    PlayerDataManager.instance.heartCollectable_Levels.Get(GameManager.instance.storySceneInt - 2));
            }
            else
            {
                heartChest.SetActive(
                    !PlayerDataManager.instance.heartCollectables_FrikinsGym.Get(frikinsGymHeartPieceNumber));
            }
        }
    }
}
