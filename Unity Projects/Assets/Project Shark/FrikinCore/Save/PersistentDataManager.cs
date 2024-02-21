using System;
using System.Collections.Generic;
using System.Linq;
using FrikinCore.Achievement;
using FrikinCore.Enumeration;
using FrikinCore.Player.Managers;
using FrikinCore.Player.Weapons;
using TSCore.Utils;

namespace FrikinCore.Save
{
    public class PersistentDataManager : Singleton<PersistentDataManager>
    {
        public static event Action OnPresetChange;

        public enum Presets
        {
            TestingBase,
            NewGame,
            BossRush,
            World1_Unlock,
            World2_Unlock,
            World3_Unlock,
            World4_Unlock,
            World5_Unlock,
            World6_Unlock,
            World7_Unlock,
            World8_Unlock,
            Frakin_Unlock,
        }

        public Presets devPreset;

        public enum DataInts
        {
            StoryHeartPieces,
            TotalTeeth,
            LevelsUnlocked,
            StoryLasersUnlocked,
            StoryPlayerMaxHealth,
            ProgenPlayerMaxHealth,
            ToothGoblinKills,
            BankSlotsBought
        }

        public enum AchievementInts
        {
            AchievementsUnlocked,
        }

        public enum DataIntArrays
        {
            NumberOfUpgradesPurchased,
            EnemyDeathCounts,
            LaserKillCounts,
        }

        public enum AchievementIntArrays
        {
        }

        public enum DataBools
        {
        }


        public enum DataBoolArrays
        {
            StoryLevelsUnlocked,
            KawaiiLevelsUnlocked,
            StoryLaserUpgradesUnlocked,
            TypesOfEnemiesThatHaveBeenDefeated,
            LaserFired,
            HeartPiecesInStoryLevelsCollected,
            HeartPiecesInTutorialCollected,
        }

        public enum AchievementBoolArrays
        {
        }

        public static Dictionary<DataInts, int> DataIntDictionary =
            new Dictionary<DataInts, int>();

        public static Dictionary<DataIntArrays, List<int>> DataIntArrayDictionary =
            new Dictionary<DataIntArrays, List<int>>();

        public static Dictionary<DataBools, bool> DataBoolDictionary =
            new Dictionary<DataBools, bool>();

        public static Dictionary<DataBoolArrays, List<bool>> DataBoolArrayDictionary =
            new Dictionary<DataBoolArrays, List<bool>>();

        public static Dictionary<AchievementInts, int> AchievementIntDictionary =
            new Dictionary<AchievementInts, int>();

        public static Dictionary<AchievementIntArrays, List<int>> AchievementIntArrayDictionary =
            new Dictionary<AchievementIntArrays, List<int>>();

        public static Dictionary<Achievements, bool> AchievementBoolDictionary =
            new Dictionary<Achievements, bool>();

        public static Dictionary<AchievementBoolArrays, List<bool>> AchievementBoolArrayDictionary =
            new Dictionary<AchievementBoolArrays, List<bool>>();

        public static int PersistentIntCount =>
            DataIntDictionary.Count + AchievementIntDictionary.Count;

        public static int PersistentIntArrayCount =>
            DataIntArrayDictionary.Count + AchievementIntArrayDictionary.Count;

        public static int PersistentBoolCount =>
            DataBoolDictionary.Count + AchievementBoolDictionary.Count;

        public static int PersistentBoolArrayCount =>
            DataBoolArrayDictionary.Count + AchievementBoolArrayDictionary.Count;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            DictionaryCreations();
        }

        public void SetDevPreset(Presets devPreset)
        {
            this.devPreset = devPreset;
            SetPresetForTesting();
        }

        void StartNewGame()
        {
            DictionaryCreations();
        }

        void SetPresetForTesting()
        {
            switch (devPreset)
            {
                case Presets.TestingBase:
                    PresetBuildSwitchSet(DataBoolArrayDictionary[DataBoolArrays.StoryLevelsUnlocked].Count - 1,
                        storyLaserIndex: 8);
                    break;
                case Presets.NewGame:
                    PresetBuildSwitchSet(1, 8, 0, 0, 1);
                    break;
                case Presets.BossRush:
                    PresetBuildSwitchSet(storyPlayerMaxHealth: 12);
                    break;
                case Presets.World1_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W1Boss + 1, 10, storyLaserIndex: 2);
                    break;
                case Presets.World2_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W2Boss + 1, 12, storyLaserIndex: 3);
                    break;
                case Presets.World3_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W3Boss + 1, 12, storyLaserIndex: 4);
                    break;
                case Presets.World4_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W4Boss + 1, 12, storyLaserIndex: 5);
                    break;
                case Presets.World5_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W5Boss + 1, 12, storyLaserIndex: 6);
                    break;
                case Presets.World6_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W6Boss + 1, 12, storyLaserIndex: 7);
                    break;
                case Presets.World7_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W7Boss + 1, 12, storyLaserIndex: 8);
                    break;
                case Presets.World8_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W8Boss + 1, 12);
                    break;
                case Presets.Frakin_Unlock:
                    PresetBuildSwitchSet((int)PlayerDataManager.LevelNames.W8Boss + 1, 12);
                    break;
            }

            OnPresetChange?.Invoke();
        }

        void PresetBuildSwitchSet(
            int levelsUnlocked = 1,
            int storyPlayerMaxHealth = 8,
            int heartPieces = 0,
            int teethTotal = 0,
            int storyLaserIndex = 8)
        {
            DataIntDictionary[DataInts.LevelsUnlocked] = levelsUnlocked;
            DataIntDictionary[DataInts.StoryPlayerMaxHealth] = storyPlayerMaxHealth;
            DataIntDictionary[DataInts.StoryHeartPieces] = heartPieces;
            DataIntDictionary[DataInts.TotalTeeth] = teethTotal;
            DataIntDictionary[DataInts.StoryLasersUnlocked] = storyLaserIndex;

            print(DataIntDictionary[DataInts.StoryHeartPieces]);
        }

        void DictionaryCreations()
        {
            //Data Creation
            foreach (var persistentDataInt in Enums.GetValues<DataInts>())
            {
                DataIntDictionary.Add(persistentDataInt, 0);
            }

            foreach (var persistentDataBool in Enums.GetValues<DataBools>())
            {
                DataBoolDictionary.Add(persistentDataBool, false);
            }

            DataIntArrayDictionary.Add(DataIntArrays.NumberOfUpgradesPurchased, PopulateListOnInitialize(0, 7));
            DataIntArrayDictionary.Add(DataIntArrays.LaserKillCounts,
                PopulateListOnInitialize(0, Enums.GetCount<WeaponName>()));
            DataIntArrayDictionary.Add(DataIntArrays.EnemyDeathCounts,
                PopulateListOnInitialize(0, Enums.GetCount<GameEnums.TypeOfEnemy>()));

            DataBoolArrayDictionary.Add(DataBoolArrays.LaserFired,
                PopulateListOnInitialize(false, Enums.GetCount<WeaponName>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.KawaiiLevelsUnlocked,
                PopulateListOnInitialize(false, Enums.GetCount<PlayerDataManager.LevelNames>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.StoryLevelsUnlocked,
                PopulateListOnInitialize(false, Enums.GetCount<PlayerDataManager.LevelNames>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.StoryLaserUpgradesUnlocked,
                PopulateListOnInitialize(false, Enums.GetCount<WeaponName>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.TypesOfEnemiesThatHaveBeenDefeated,
                PopulateListOnInitialize(false, Enums.GetCount<GameEnums.TypeOfEnemy>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.HeartPiecesInStoryLevelsCollected,
                PopulateListOnInitialize(false,
                    Enums.GetCount<PlayerDataManager.HeartCollectables_Levels>()));
            DataBoolArrayDictionary.Add(DataBoolArrays.HeartPiecesInTutorialCollected,
                PopulateListOnInitialize(false,
                    Enums.GetCount<PlayerDataManager.HeartCollectables_FrikinsGym>()));

            SetPresetForTesting();
        }

        List<T> PopulateListOnInitialize<T>(T value, int count)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(value);
            }

            return list;
        }

        public void SaveData()
        {
            SaveManager.SavePlayer(this);
        }

        public void LoadData()
        {
            SaveManager.LoadGameData();
            PlayerData playerData = SaveManager.playerData;
            for (int i = 0; i < DataIntDictionary.Count; i++)
            {
                DataIntDictionary[(DataInts)i] = playerData.SavedInts[i];
            }

            for (int i = 0; i < AchievementIntDictionary.Count; i++)
            {
                AchievementIntDictionary[(AchievementInts)i] = playerData.SavedInts[i + DataIntDictionary.Count];
            }

            //Bools
            for (int i = 0; i < DataBoolDictionary.Count; i++)
            {
                DataBoolDictionary[(DataBools)i] = playerData.SavedBools[i];
            }

            //Int[]
            for (int i = 0; i < DataIntArrayDictionary.Count; i++)
            {
                DataIntArrayDictionary[(DataIntArrays)i] = playerData.SavedIntArrays[i].ToList();
            }

            for (int i = 0; i < AchievementIntArrayDictionary.Count; i++)
            {
                AchievementIntArrayDictionary[(AchievementIntArrays)i] =
                    playerData.SavedIntArrays[i + DataIntArrayDictionary.Count].ToList();
            }

            //bool[]
            for (int i = 0; i < DataBoolArrayDictionary.Count; i++)
            {
                DataBoolArrayDictionary[(DataBoolArrays)i] = playerData.SavedBoolArrays[i].ToList();
            }

            for (int i = 0; i < AchievementBoolArrayDictionary.Count; i++)
            {
                AchievementBoolArrayDictionary[(AchievementBoolArrays)i] =
                    playerData.SavedBoolArrays[i + DataBoolArrayDictionary.Count].ToList();
            }
        }

        public void ClearGameData()
        {
            DataBoolDictionary.Clear();
            DataIntDictionary.Clear();
            DataBoolArrayDictionary.Clear();
            DataIntArrayDictionary.Clear();
            AchievementBoolDictionary.Clear();
            AchievementIntDictionary.Clear();
            AchievementBoolArrayDictionary.Clear();
            AchievementIntArrayDictionary.Clear();

            DictionaryCreations();
            //SaveData();
        }
    }
}