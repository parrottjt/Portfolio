using FrikinCore.Sfx;

namespace FrikinCore.GameInformation
{
    public class GameState
    {
        public readonly GameStates LevelGameState;
        public readonly int WorldNumber;
        public readonly int StateSceneNumber;
        public readonly bool IsBossScene;
        public readonly float TimeToCompleteLevel;
        public readonly int BossMusicIndex;

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber,
            bool _isBossScene, BossMusic musicIndex, float _timeToCompleteLevel = 0)
        {
            LevelGameState = levelGameState;
            WorldNumber = _worldNumber;
            StateSceneNumber = _stateSceneNumber;
            IsBossScene = _isBossScene;
            TimeToCompleteLevel = _timeToCompleteLevel;
            BossMusicIndex = (int)musicIndex;
        }

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber, BossMusic musicIndex)
            : this(levelGameState, _worldNumber, _stateSceneNumber, false, musicIndex, 0)
        {
        }

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber, bool _isBossScene)
            : this(levelGameState, _worldNumber, _stateSceneNumber, _isBossScene, (BossMusic)0, 0)
        {
        }

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber, float _timeToCompleteLevel,
            bool _isBossScene)
            : this(levelGameState, _worldNumber, _stateSceneNumber, _isBossScene, (BossMusic)0, _timeToCompleteLevel)
        {
        }

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber, float _timeToCompleteLevel)
            : this(levelGameState, _worldNumber, _stateSceneNumber, false, 0, _timeToCompleteLevel)
        {
        }

        public GameState(GameStates levelGameState, int _worldNumber, int _stateSceneNumber)
            : this(levelGameState, _worldNumber, _stateSceneNumber, false, 0, 0)
        {
        }
    }
}