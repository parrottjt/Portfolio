using FrikinCore.Sfx;
using UnityEngine;

namespace FrikinCore.ScenesManagement
{
    [CreateAssetMenu(fileName = "Scene Info", menuName = "Game Data/Scene Info")]

    public class SceneInformation : ScriptableObject
    {
        enum Worlds
        {
            Tutorial,
            World1,
            World2,
            World3,
            World4,
            World5,
            World6,
            World7,
            World8,
            Frakin,
            Hub
        }

        [SerializeField] string sceneName;
        [SerializeField] GameStates levelGameState = GameStates.Story;
        [SerializeField] Worlds world = Worlds.World1;
        [SerializeField] bool isBossScene = false;
        [SerializeField] bool hasMiniBoss;
        [SerializeField] bool isMiniGame = false;
        [SerializeField] float speedRunTimeCompletion;

        [SerializeField] [HideInInspector] bool hasBossMusic; //This has to be both so the DrawIf works

        [SerializeField] [DrawIf("hasBossMusic", true)]
        BossMusic bossMusic = BossMusic.None;

        public string SceneName => sceneName;
        public GameStates LevelGameState => levelGameState;
        public int WorldNumber => (int)world;
        public bool IsBossScene => isBossScene;
        public bool HasMiniBoss => hasMiniBoss;
        public bool IsMiniGame => isMiniGame;
        public float SpeedRunTimeCompletion => speedRunTimeCompletion;
        public BossMusic BossMusic => bossMusic;

        void OnValidate()
        {
            hasBossMusic = isBossScene || hasMiniBoss;
            if (!hasBossMusic) bossMusic = BossMusic.None;
        }
    }
}
