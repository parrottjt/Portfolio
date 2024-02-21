using System;
using System.Collections.Generic;
using System.Linq;
using FrikinCore.AI.Enemy.Health;
using FrikinCore.DevelopmentTools;
using FrikinCore.GameInformation;
using FrikinCore.Loot;
using FrikinCore.Loot.Teeth;
using FrikinCore.Managers;
using FrikinCore.Player;
using FrikinCore.Player.Weapons;
using FrikinCore.Respawn;
using FrikinCore.ScenesManagement;
using FrikinCore.Score;
using FrikinCore.Sfx;
using TSCore.Time;
using TSCore.Utils;
using UICore;
using UICore.SharkFact;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrikinCore
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public class CallOnDestroyEventArgs : EventArgs
        {
        }

        public static event EventHandler<CallOnDestroyEventArgs> CallOnDestroy;
        public static event Action CallOnSceneChange;

        #endregion

        #region Dictionaries

        public static Dictionary<string, SceneInformation> SceneInformationDictionary =
            new Dictionary<string, SceneInformation>();

        public static Dictionary<string, GameState> gameStateLevelInfo = new Dictionary<string, GameState>
        {
            //Menu Scenes
            { "Menu", new GameState(GameStates.Menu, 0, 0) },
            { "EndingScene", new GameState(GameStates.Menu, 0, 1) },
            { "Death", new GameState(GameStates.Menu, 0, 2) },
            { "Winning_Hashtag", new GameState(GameStates.Menu, 0, 3) },
            { "LoadingScreen", new GameState(GameStates.Menu, 0, 4) },

            //Story Scenes
            { "Frikins Gym", new GameState(GameStates.Story, 0, 0) },
            { "GoldFishBoss", new GameState(GameStates.Story, 0, 1, true, BossMusic.Miniboss) },
            { "World 1-1", new GameState(GameStates.Story, 0, 2, BossMusic.Miniboss) },
            { "World 1-2", new GameState(GameStates.Story, 0, 3) },
            { "World 1-3", new GameState(GameStates.Story, 0, 4) },
            { "Octoboss", new GameState(GameStates.Story, 0, 5, true, BossMusic.Octoboss) },
            { "World 2-1", new GameState(GameStates.Story, 1, 6) },
            { "World 2-2", new GameState(GameStates.Story, 1, 7, BossMusic.Miniboss) },
            { "World 2-3", new GameState(GameStates.Story, 1, 8) },
            { "Puff Daddy", new GameState(GameStates.Story, 1, 9, true, BossMusic.PuffDaddy) },
            { "World 3-1", new GameState(GameStates.Story, 2, 10) },
            { "World 3-2", new GameState(GameStates.Story, 2, 11, BossMusic.Miniboss) },
            { "World 3-3", new GameState(GameStates.Story, 2, 12) },
            { "She-Reks", new GameState(GameStates.Story, 2, 13, true, BossMusic.She_Rex) },
            { "World 4-1", new GameState(GameStates.Story, 3, 14) },
            { "World 4-2", new GameState(GameStates.Story, 3, 15, BossMusic.Miniboss) },
            { "World 4-3", new GameState(GameStates.Story, 3, 16) },
            { "Bionic Prawn", new GameState(GameStates.Story, 3, 17, true, BossMusic.BionicPrawn) },
            { "World 5-1", new GameState(GameStates.Story, 4, 18) },
            { "World 5-2", new GameState(GameStates.Story, 4, 19, BossMusic.Miniboss) },
            { "World 5-3", new GameState(GameStates.Story, 4, 20) },
            { "Eel-etric Boogaloo", new GameState(GameStates.Story, 4, 21, true, BossMusic.Eel) },
            { "World 6-1", new GameState(GameStates.Story, 5, 22) },
            { "World 6-2", new GameState(GameStates.Story, 5, 23, BossMusic.Miniboss) },
            { "World 6-3", new GameState(GameStates.Story, 5, 24) },
            { "Pirate Ship", new GameState(GameStates.Story, 5, 25, true, BossMusic.PirateShip) },
            { "World 7-1", new GameState(GameStates.Story, 6, 26) },
            { "World 7-2", new GameState(GameStates.Story, 6, 27, BossMusic.Miniboss) },
            { "World 7-3", new GameState(GameStates.Story, 6, 28) },
            { "Killy", new GameState(GameStates.Story, 6, 29, true, BossMusic.Killy) },
            { "World 8-1", new GameState(GameStates.Story, 7, 30) },
            { "World 8-2", new GameState(GameStates.Story, 7, 31, BossMusic.Miniboss) },
            { "World 8-3", new GameState(GameStates.Story, 7, 32) },
            { "Shark Family", new GameState(GameStates.Story, 7, 33, true, BossMusic.SharkFamily) },
            { "Frakin", new GameState(GameStates.Story, 7, 34, true, BossMusic.Frakin) },
            { "Trash Monster Test", new GameState(GameStates.Story, 0, 35, true) },

            //Boss Rush Scenes
            { "GoldFishBoss Boss Rush", new GameState(GameStates.BossRush, 0, 0, true, BossMusic.Miniboss) },
            { "SenseiStarfish Boss Rush", new GameState(GameStates.BossRush, 0, 1, true, BossMusic.Miniboss) },
            { "Octoboss Boss Rush", new GameState(GameStates.BossRush, 0, 2, true, BossMusic.Octoboss) },
            { "Glockadile Boss Rush", new GameState(GameStates.BossRush, 1, 3, true, BossMusic.Miniboss) },
            { "PuffDaddy Boss Rush", new GameState(GameStates.BossRush, 1, 4, true, BossMusic.PuffDaddy) },
            { "TankSteg Boss Rush", new GameState(GameStates.BossRush, 2, 5, true, BossMusic.Miniboss) },
            { "She-Reks Boss Rush", new GameState(GameStates.BossRush, 2, 6, true, BossMusic.She_Rex) },
            { "Eel-etric Boogaloo Boss Rush", new GameState(GameStates.BossRush, 4, 7, true, BossMusic.Eel) },
            { "HammerHead Shark Boss Rush", new GameState(GameStates.BossRush, 6, 8, true, BossMusic.Miniboss) },
            { "Killy Boss Rush", new GameState(GameStates.BossRush, 6, 9, true, BossMusic.Killy) },
            { "Shark Family Boss Rush", new GameState(GameStates.BossRush, 7, 10, true, BossMusic.SharkFamily) },
            { "Frakin Boss Rush", new GameState(GameStates.BossRush, 7, 11, true, BossMusic.Frakin) },
        };

        public static Dictionary<GameStates, bool> GameStatesDictionary = new Dictionary<GameStates, bool>
        {
            { GameStates.Menu, false },
            { GameStates.Story, false },
            { GameStates.BossRush, false },
            { GameStates.ProGen, false }
        };

        public static Dictionary<Settings, bool> GameSettings = new Dictionary<Settings, bool>
        {
            { Settings.HasSceneRestarted, false },
            { Settings.PauseMenu, false },
            { Settings.CanHeal, false },
            { Settings.CanMove, false },
            { Settings.CanShoot, false },
            { Settings.MiniBossActive, false },
            { Settings.IsBossScene, false }
        };

        Dictionary<GameStates, IRespawn> respawnBehaviors = new Dictionary<GameStates, IRespawn>
        {
            { GameStates.Menu, null },
            { GameStates.Story, new StoryRespawn() },
            { GameStates.BossRush, new BossRushRespawnBehavior() }
        };

        #endregion

        #region Scripts
        [HideInInspector] public ScoringManager scoringManagerCode;
        [HideInInspector] public SetAudioLevel setAudio;
        [HideInInspector] public LootManager loot;
        [HideInInspector] public BossRushManager bossRushManager;
        [HideInInspector] public WeaponList weaponList;
        #endregion

        List<HealthEnemy> listOfEnemies = new List<HealthEnemy>();

        public int storySceneInt, progenSceneInt, menuSceneInt, bossRushSceneInt;
        int worldNumber, bossMusicNumber;
        int tickForDamage, tick;

        public bool inProGen;

        public GameObject dodgeTutTextbox;

        GameObject backgroundManager;

        public string currentScene = "";

        public static GameStates gameState;

        [SerializeField] GameObject[] managerPrefabs;
        List<GameObject> currentInstancedPrefabs = new List<GameObject>();

        Camera mainCamera;

        public int EnemyAliveCount => listOfEnemies.Count(enemy => !enemy.IsDead);
        public int WorldNumber => worldNumber;
        public int BossMusicNumber => bossMusicNumber;
        public Camera MainCamera => mainCamera;


        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();

            CreateDictionaryInstances();
            InstanceManagerPrefab();

            DontDestroyOnLoad(gameObject);

            #region Set Code/Objects

            loot = GetComponent<LootManager>();
            scoringManagerCode = GetComponent<ScoringManager>();
            setAudio = GetComponent<SetAudioLevel>();
            bossRushManager = GetComponent<BossRushManager>();
            weaponList = GetComponent<WeaponList>();

            #endregion
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;

#if UNITY_EDITOR
            Cursor.lockState = CursorLockMode.None;
#endif

            //This is only here for unity testing purposes, to setup the scene name from the scene you start at
            SceneManagement.SetCurrentLevelName(SceneManager.GetActiveScene().name);

            LevelNameCheck();

            TickTimeTimer.OnTick += OnTick;
            TickTimeTimer.OnTick_OneSec += OnTick_OneSec;
            CallOnSceneChange += SceneChange;

            CallOnSceneChange?.Invoke();
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            ///This if had to be replaced as changing it to just a bool that is changed in the SceneManagement
            /// broke everything. I want to look into changing this and finding out fully why it broke everything.
            /// It was running the CallOnSceneChange 38 times which created so many calls that it broke the music
            /// and the framerate.
            /// Possible solutions will be to figure out exactly when the scenes name has changed
            if (SceneManager.GetActiveScene().name != currentScene)
            {
                LevelNameCheck();
                
                mainCamera = Camera.main;
                CallOnSceneChange?.Invoke();
            }

            if (!GameStatesDictionary[GameStates.Menu])
            {
                if (!GameSettings[Settings.IsBossScene])
                {
                    UIManager.instance.InfoHolder.bossHealthBar.SetActive(GameSettings[Settings.MiniBossActive]);
                }
            }
        }

        /// <summary>
        /// This check runs checks through each of the arrays that hold the scene names
        /// to check which type of scene the player is currently in
        ///
        /// </summary>
        void LevelNameCheck()
        {
            SceneInformation levelGameState;
            if (!SceneInformationDictionary.ContainsKey(SceneManagement.CurrentLevelName))
            {
                print(
                    "SceneManager.GetActiveScene().name has not been setup in the gameStateLevelInfo.");
                levelGameState = SceneInformationDictionary["World 1-1"];
            }
            else levelGameState = SceneInformationDictionary[SceneManager.GetActiveScene().name];


            gameState = levelGameState.LevelGameState;
            worldNumber = levelGameState.WorldNumber;
            bossMusicNumber = (int)levelGameState.BossMusic;
            GameSettings[Settings.IsBossScene] = levelGameState.IsBossScene;
            GameStatesDictionary[GameStates.Story] = gameState == GameStates.Story || gameState == GameStates.BossRush;
            GameStatesDictionary[GameStates.Menu] = gameState == GameStates.Menu;
            GameStatesDictionary[GameStates.BossRush] = gameState == GameStates.BossRush;

            loot.enabled = !GameStatesDictionary[GameStates.Menu];

            SharkFactController.BuildSharkFactList();

//This will trigger boss rush scenemanagement to turn on and off
            bossRushManager.enabled = GameStatesDictionary[GameStates.BossRush];
            switch (gameState)
            {
                case GameStates.Story:
                    //storySceneInt = levelGameState.StateSceneNumber;
                    loot.SetTimeToCompleteLevel(levelGameState.SpeedRunTimeCompletion);
                    if (levelGameState.SpeedRunTimeCompletion <= 0)
                        print($"{SceneManager.GetActiveScene().name} time to complete hasn't been setup");
                    break;
                case GameStates.Menu:
                    //menuSceneInt = levelGameState.StateSceneNumber;
                    GameSettings[Settings.MiniBossActive] = false;
                    PlayerManager.instance.WeaponManagement.WeaponReload(AmmoHandler.ReloadAmmoAmount.ReloadAllFull);
                    break;
                case GameStates.ProGen:
                    break;
                case GameStates.BossRush:
                    //bossRushSceneInt = levelGameState.StateSceneNumber;
                    break;
            }
        }

        public bool TickDamage()
        {
            if (tickForDamage < 1) return false;
            tickForDamage = 0;
            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            CallOnDestroy?.Invoke(this, new CallOnDestroyEventArgs());
            foreach (var currentInstancedPrefab in currentInstancedPrefabs)
            {
                Destroy(currentInstancedPrefab);
            }

            currentInstancedPrefabs.Clear();
        }

        void OnTick_OneSec(object sender, TickTimeTimer.OnTickEventArgs args) => tickForDamage++;

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            if (!GameSettings[Settings.HasSceneRestarted])
            {
                tick++;
                if (tick >= 3)
                {
                    GameSettings[Settings.HasSceneRestarted] = true;
                    tick = 0;
                }
            }
        }

        void SceneChange()
        {
            GameSettings[Settings.HasSceneRestarted] = false;
            currentScene = SceneManagement.CurrentLevelName;
            SoundManager.instance.PlayBackgroundMusic();
            ToothPlacement.PlaceCorrectToothTypeWhereTileMapPlacementIs();
        }

        void InstanceManagerPrefab()
        {
            GameObject prefab;
            for (int i = 0;
                 i < managerPrefabs.Length;
                 i++)
            {
                prefab = Instantiate(managerPrefabs[i]);
                currentInstancedPrefabs.Add(prefab);
            }
        }

        void CreateDictionaryInstances()
        {
            var sceneInformation = Resources.LoadAll<SceneInformation>("Scene Info");
            foreach (var sceneInfo in sceneInformation)
            {
                if (SceneInformationDictionary.ContainsKey(sceneInfo.SceneName) == false)
                {
                    SceneInformationDictionary.Add(sceneInfo.SceneName, sceneInfo);
                }
                else
                {
                    DebugScript.Log(typeof(GameManager),
                        $"Scene Information Dictionary already contains key {sceneInfo.SceneName}");
                }
            }
        }

        void GetEnemyList()
        {
            if (listOfEnemies.Count > 0)
            {
                listOfEnemies.Clear();
            }

            foreach (var enemy in FindObjectsOfType<HealthEnemy>())
            {
                listOfEnemies.Add(enemy);
            }
        }
    }

    public enum GameTags
    {
        Player,
        Enemy,
        NavPoint,
        PickUp,
        Scoring,
        boom,
        ExitPortal,
        Destructable,
        Slowing,
        Spikes,
        hide,
        MoveableObj,
        cameraBorder,
        Projectile,
        Lazer,
        Boss,
        Indicator,
        Border,
        Created,
        LaserDestroy,
        Shield,
        AmmoPickup,
        NavTrigger,
        AmmoShotty,
        AmmoBurst,
        AmmoCharge,
        Current,
        GameManager,
        Bangarang,
        heartReplacement,
        heartHolder,
        onlyTrigger,
        enemyBorder,
        BouncePad,
        SpringArm,
        DamagingSeaweed,
        Reflecting,
        SpikyPoofs,
        ProjectileHolder,
        Hazard
    }

    public enum Settings
    {
        CanHeal,
        CanShoot,
        CanMove,
        PauseMenu,
        HasSceneRestarted,
        MiniBossActive,
        IsBossScene,
        BugReporter,
    }

    public enum GameStates
    {
        Menu,
        Story,
        ProGen,
        BossRush
    }
}