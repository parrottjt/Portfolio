using System;
using TSCore.Utils;

namespace FrikinCore.Player.Managers
{
    public partial class PlayerDataManager : Singleton<PlayerDataManager>
    {
        /*
    public static Dictionary<Achievements, AchievementHandler> AchievementHandlers =
        new Dictionary<Achievements, AchievementHandler>();

    [HideInInspector] public bool levelSelectOpen;

    #region Save/Load Testing Declarations

    [Header("Use For Save/Load Testing Only")]
    private Animator _saveAnimator;

    [FormerlySerializedAs("saveAnimHolder")] 
    public GameObject saveAnimatorHolder; 
    
    #endregion
    
    public enum Presets
    {
        TestingBase,
        FreshStart,
        BossRush,
        World1_Unlock,
        World2_Unlock,
        World3_Unlock,
        
    }

    [FormerlySerializedAs("preset")] 
    public Presets devPreset; 

    #region Stat Variables

    [Header("Stats")] public int levelsUnlocked = 1;

    public int
        sandDollars,
        proGenHealth = 60,
        storyHealth = 100,
        storyLasersIndex;
    
    public bool[] gunsFired;

    public bool allGunsFired;

    public int teethTotal, heartPieces, bankSlotsBought; //Moved

    public float[] permStats; //Delete this

    [HideInInspector] public float[] percentsForStats =
    {
        .1f, .5f, .1f, .5f, .5f, .3f, .5f //Don't delete these just yet
    };

    public int[] numPurchased; //Do Not DELETE THIS

    [Header("Upgrade Stuff")]
    //This needs only one array built into it
    public UpgradeCosts[] cost;

    #endregion

    #region Achievement Variables

    [Header("Achievement bools and such")]
    //Red Lazer Only Achievements
    public bool onlyRedBossAchieved; //Moved

    public bool noRedLaserAchievementStatus, //Moved
        onlyRedFrakinAchieved, //Moved
        noRedLaserFrakinAchievementStatus, //Moved
        gunSwaped; //Doesn't need to save

    //No Dashing For entire level Achievement
    public bool noDashAchieved, //Moved
        noDashAchievementStatus; //Doesn't need to saved
    private string lastScene = ""; //Have Reference saved in SceneManagement
    public bool wasInMenu; //Doesn't need to save

    public int toothGoblinKillCount; //Moved

    //Finish Tutorial
    public bool tutorialCompleted; //Moved

    //First Progen Victory
    public bool firstProgenVictory; //Moved

    //EnemyDeaths
    public bool[] hasEnemyDied; //Moved
    public int deadIndex; //Not sure what this was going to be used for

    public bool allEnemiesDead; //Moved

    //For the individual count of an enemy get the index from the enemy in Parker_EnemyHealth
    public int[] enemyDeathCount; //Moved


    //island of lost toys
    public bool teethOver9000ach, //Moved
        allStoryHeartsAch, //Moved
        allStoryLasersFiredOnceAch, //Moved
        firstStoryHeartPiece, //Moved
        firstSandDollar, //Moved
        firstBankUse, //Moved
        maxBounceKills, //Moved
        shottyTripleKill, //Moved
        senseiNinjaKill, //Moved
        firstFivePiece, //Moved
        firstProgenHealthIncrease, //Moved
        firstHordeWin, //Moved
        kawaiiVictory, //Moved
        hundMiniBerthaKills, //Moved
        hundTeethGunKills, //Moved
        hundPewPooKills, //Moved
        hundSharkRepellentKills, //Moved
        hundFlamethrowerKills, //Moved
        fullUpgradeProgen, //Moved
        allBankSlotsBought, //Moved
        thousandSandDollars, //Moved
        killLootGoblin, //Moved
        blazeFishKill, //Moved
        killMommaSharkFirst, //Moved
        killAnglerFish, //Moved
        firstItemReroll, //Moved
        fullyUpgradeItem, //Moved
        frakinDefeated, //Moved
        finalHordeWin, //Moved
        perfectNumber; //Moved

    public bool[] achievementUnlockArray;
    public bool achievementUnlocked;

    public int platTrueInt, //Moved
        lootGoblinDeaths, //Moved
        miniBerthaKills, //Moved
        teethGunKills, //Moved
        pooPewKills, //Moved
        sharkRepellentKills, //Moved
        flamethrowerKills, //Moved
        shottyTripleKillsCounter; //Moved

    //This is the same array in steam savedFloats but the reference hasn't been changed

    #endregion
    private float AcheivementCheckTimer;
    [ReadOnly] public bool coopActive;
 
    

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        levelsUnlocked = (int) normLevelNames + 1;

        normalLevelArray = new BitArray((int) LevelNames_Normal.Frakin + 1);
        heartCollectable_Levels = new BitArray((int) HeartCollectables_Levels.Frakin + 1);
        heartCollectables_FrikinsGym = new BitArray((int) HeartCollectables_FrikinsGym.FG4 + 1);
        achievementUnlockArray = new bool[(int) Achievements.ACH_PERFECT_NUMBER_PLUS_ONE + 1];

        permStats = new float[(int) GameEnums.PermanentStats.InvulnFrameAdjust + 1];
        for (int i = 0; i < permStats.Length; i++)
        {
            permStats[i] = 1;
        }

        bank.bankedItemA = -1;
        bank.bankedItemB = -1;
        bank.bankedItemC = -1;
        bank.bankedItemD = -1;
        bank.bankedItemE = -1;

        #region Array Length Sets

        gunsFired = new bool[9];
        hasEnemyDied = new bool[39];
        enemyDeathCount = new int[39];

        #endregion

        percentsForStats = new float[]
        {
            .1f, .5f, .1f, .5f, .5f, .3f, .5f
        };
    }

    void Start()
    {
        PresetBuildSwitch();
        SoundManager.instance.PlayBackgroundMusic();
        
        // This may change to a reflection call
        // var enums = Wehking_EnumUtils.GetValues<Achievements>();
        //  for (var i = 0; i < enums.Length; i++)
        //  {
        //      var achievementEnum = enums[i];
        //      achievementHandlers.Add(achievementEnum, new AchievementHandler(achievementEnum));
        //      achievementHandlers[(Achievements)i].SetAchievement();
        //      print(achievementHandlers[(Achievements) i].HasAchievementBeenAchieved());
        //  }
        
        //I am removing this for now, we can turn it back on after the June 3rd Build
        //TickTimeTimer.OnTick += OnTickOnTick_OneSec;
    }

    private void Update()
    {
        Mathf.Clamp(levelsUnlocked, 1, (int) LevelNames_Normal.Frakin + 1);

        //This will hold the current level the player is on, which will be needed for a continue button
        normLevelNames = (LevelNames_Normal) (int) levelsUnlocked - 1;

        //Update Debug Test

        //This needs to be moved out of this script
        if (GameManager.GameStatesDictionary[GameStates.Story])
        {
            if (!gunsFired[PlayerManager.instance.WeaponManagement.WeaponIndex] && InputManager.instance.GetFireInput())
            {
                gunsFired[PlayerManager.instance.WeaponManagement.WeaponIndex] = true;
                Helper_Conditions.AllConditionMatchValue(gunsFired);
            }
        }
        
        if (GameManager.IsInitialized)
        {
            #region Heart Pickups

            if (GameManager.GameStatesDictionary[GameStates.Story] && !GameManager.GameSettings[Settings.IsBossScene])
            {
                if (Holder == null)
                    Holder = GameObject.FindGameObjectWithTag("heartHolder");

                if (Replacement == null)
                    Replacement = GameObject.FindGameObjectWithTag("heartReplacement");

                if (frikinGymHeart1Holder == null)
                    frikinGymHeart1Holder = GameObject.Find("frikinsGymFirstHeartHolder");

                if (frikinGymHeart2Holder == null)
                    frikinGymHeart2Holder = GameObject.Find("frikinsGymSecondHeartHolder");

                if (frikinGymHeart3Holder == null)
                    frikinGymHeart3Holder = GameObject.Find("frikinsGymThirdHeartHolder");

                if (frikinGymHeart4Holder == null)
                    frikinGymHeart4Holder = GameObject.Find("frikinsGymFourthHeartHolder");

                if (!GameManager.GameSettings[Settings.HasSceneRestarted] ||
                    GameManager.instance.playerCode.isDeadSharky)
                {
                    if (GameManager.instance.storySceneInt > 2)
                    {
                        Holder.SetActive(!heartCollectable_Levels.Get(GameManager.instance.storySceneInt - 2));
                        Replacement.SetActive(heartCollectable_Levels.Get(GameManager.instance.storySceneInt - 2));
                    }

                    if (GameManager.instance.storySceneInt == 0)
                    {
                        frikinGymHeart1Holder.SetActive(!heartCollectables_FrikinsGym.Get(0));
                        frikinGymHeart2Holder.SetActive(!heartCollectables_FrikinsGym.Get(1));
                        frikinGymHeart3Holder.SetActive(!heartCollectables_FrikinsGym.Get(2));
                        frikinGymHeart4Holder.SetActive(!heartCollectables_FrikinsGym.Get(3));
                    }
                }
            }

            #endregion
        }

        #region Auto Save Anim Finds

        if (saveAnimatorHolder == null)
            saveAnimatorHolder = GameObject.Find("AutoSave");

        if (_saveAnimator == null)
            _saveAnimator = saveAnimatorHolder.GetComponent<Animator>();

        #endregion
    }

    //Commented out the achievement check as we need to redo the achievements and
    // also to not sure if this is causing an issue.
    void OnTickOnTick_OneSec(object sender, TickTimeTimer.OnTickEventArgs args)
    {
        // if (AcheivementCheckTimer > 120)
        // {
        //     AchievementUnlockCheck();
        //     AcheivementCheckTimer = 0;
        // }
        // else AcheivementCheckTimer++;
    }

    private void AchievementUnlockCheck()
    {
        if (GameManager.GameStatesDictionary[GameStates.Menu])
            wasInMenu = true;

        if (wasInMenu && !GameManager.GameStatesDictionary[GameStates.Menu])
        {
            lastScene = SceneManager.GetActiveScene().name;
            wasInMenu = false;
        }

        if (GameManager.GameStatesDictionary[GameStates.Story])
        {
            #region Achievement Updates

            #region Red Laser Only Achievements

            if (!achievementUnlockArray[(int) Achievements.ACH_DEFEAT_BOSS_RED_LASER])
                if (GameManager.GameSettings[Settings.IsBossScene])
                    if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName != WeaponNames.NormalLazer &&
                        InputManager.instance.GetFireInput())
                        gunSwaped = true;

            if (!achievementUnlockArray[(int) Achievements.ACH_DEFEAT_FRAKIN_WITH_ONLY_RED_LAZER])
            {
                if (SceneManager.GetActiveScene().name == "Frakin")
                    if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName != WeaponNames.NormalLazer &&
                        InputManager.instance.GetFireInput())
                        gunSwaped = true;
            }

            #endregion

            #region No Dashing During Level

            if (!achievementUnlockArray[(int) Achievements.ACH_COMPLETE_LEVEL_WITHOUT_DASHING])
            {
                if (GameManager.instance.playerCode.GetIsDashing())
                    noDashAchievementStatus = true;

                if (lastScene != SceneManager.GetActiveScene().name && !noDashAchievementStatus && !wasInMenu)
                    if (UIManager.instance.InfoHolder.rewardPanel.activeSelf)
                        achievementUnlockArray[(int) Achievements.ACH_COMPLETE_LEVEL_WITHOUT_DASHING] = true;

                if (lastScene != SceneManager.GetActiveScene().name && noDashAchievementStatus && !wasInMenu)
                {
                    lastScene = SceneManager.GetActiveScene().name;
                    noDashAchievementStatus = false;
                }
            }

            #endregion

            // will need to be updated if we change achievement requirements

            #region Teeth Over 9000

            if (!achievementUnlockArray[(int) Achievements.ACH_9001TEETH])
                if (teethTotal >= 9001)
                    achievementUnlockArray[(int) Achievements.ACH_9001TEETH] = true;

            #endregion

            //This needs to be updated, (Total)

            #region all story hearts

            if (!achievementUnlockArray[(int) Achievements.ACH_MAX_HEARTS_IN_STORY_MODE])
                if (storyHealth >= 29)
                    achievementUnlockArray[(int) Achievements.ACH_MAX_HEARTS_IN_STORY_MODE] = true;

            #endregion

            #region all story lasers fired once

            if (!achievementUnlockArray[(int) Achievements.ACH_ALL_WEAPONS_USED])
                if (allGunsFired)
                    achievementUnlockArray[(int) Achievements.ACH_ALL_WEAPONS_USED] = true;

            #endregion

            #region first story heart piece

            if (!achievementUnlockArray[(int) Achievements.ACH_FIRST_HEART_PIECE_COLLECTED])
                if (heartPieces >= 1)
                    achievementUnlockArray[(int) Achievements.ACH_FIRST_HEART_PIECE_COLLECTED] = true;

            #endregion

            #region kill counts

            #region kill count achievement bool checks

            if (!hundSharkRepellentKills)
                if (sharkRepellentKills >= 100)
                    achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_SHARK_REPELLENT] = true;

            if (!hundMiniBerthaKills)
                if (miniBerthaKills >= 100)
                    achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_MINI_BERTHA] = true;

            if (!hundFlamethrowerKills)
                if (flamethrowerKills >= 100)
                    achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_FLAMETHROWER] = true;

            if (!hundPewPooKills)
                if (pooPewKills >= 100)
                    achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_POO_PEW] = true;

            if (!achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_TEETH_GUN])
                if (teethGunKills >= 100)
                    achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_TEETH_GUN] = true;

            #endregion

            #endregion

            #region Perfect Number Plus One Achievement

            for (int i = 0; i < achievementUnlockArray.Length; i++)
            {
                if (achievementUnlockArray[i])
                    platTrueInt++;

                if (platTrueInt == achievementUnlockArray.Length - 1)
                    achievementUnlockArray[(int) Achievements.ACH_PERFECT_NUMBER_PLUS_ONE] = true;

                if (i == achievementUnlockArray.Length - 1 && platTrueInt != achievementUnlockArray.Length - 1)
                    platTrueInt = 0;
            }

            #endregion

            #region ProGen Achievements

            #region first Sand Dollar

            if (GameManager.GameStatesDictionary[GameStates.ProGen])
            {
                if (!firstSandDollar)
                {
                    if (sandDollars >= 1)
                        achievementUnlockArray[(int) Achievements.ACH_FIRST_SAND_DOLLAR] = true;
                }

                #endregion

                #region progen health increase

                if (!firstProgenHealthIncrease)
                {
                    if (proGenHealth > 60f)
                        achievementUnlockArray[(int) Achievements.ACH_INCREASE_PROGEN_HEALTH_FOR_FIRST_TIME] = true;
                    ;
                }

                #endregion

                #region 1000 sand dollars

                if (!thousandSandDollars)
                {
                    if (sandDollars >= 1000)
                        achievementUnlockArray[(int) Achievements.ACH_THOUSAND_SAND_DOLLARS_SAVED] = true;
                }

                #endregion

                #region bank slot ach

                if (!allBankSlotsBought && bankSlotsBought >= 5)
                    achievementUnlockArray[(int) Achievements.ACH_PURCHASE_ALL_BANK_SLOTS] = true;

                #endregion

                #region proGen misc

                if (!firstProgenVictory)
                {
                    if (SceneManager.GetActiveScene().name == "Progen Win Screen")
                        achievementUnlockArray[(int) Achievements.ACH_COMPLETE_PROGEN] = true;
                }
            }

            #endregion

            #endregion

            #endregion
        }
    }

    public void Save()
    {
        SaveManager.SavePlayer(this);
        if (saveAnimatorHolder != null)
        {
            _saveAnimator.Play("autoSave");
            Debug.Log("you saved your game");
        }
    }

    public void Load()
    {
        PlayerData playerdata = SaveManager.playerData;

        #region Array Declarations

        int[] loadedAchInts = playerdata.savedInts;
        bool[] loadedAchBools = playerdata.ACH_Bools;

        #endregion
        
        kawaiiMode = loadedAchBools[1];
        
        #region Bank

        bank.bankedItemA = loadedAchInts[0];
        bank.bankedItemB = loadedAchInts[1];
        bank.bankedItemC = loadedAchInts[2];
        bank.bankedItemD = loadedAchInts[3];
        bank.bankedItemE = loadedAchInts[4];

        // savedFloats
        teethTotal = loadedAchInts[5];
        heartPieces = loadedAchInts[6];
        bankSlotsBought = loadedAchInts[7];
        numPurchased[(int) GameEnums.PermanentStats.WeaponDamageAdjust] = loadedAchInts[15];
        numPurchased[(int) GameEnums.PermanentStats.WeaponRangeAdjust] = loadedAchInts[16];
        numPurchased[(int) GameEnums.PermanentStats.MaxAmmoAdjust] = loadedAchInts[17];
        numPurchased[(int) GameEnums.PermanentStats.IFramesAdjust] = loadedAchInts[18];
        numPurchased[(int) GameEnums.PermanentStats.ShadowShotAdjust] = loadedAchInts[19];
        numPurchased[(int) GameEnums.PermanentStats.ReloadCooldownAdjust] = loadedAchInts[20];
        numPurchased[(int) GameEnums.PermanentStats.DropRateAdjust] = loadedAchInts[21];

        #endregion

        #region Achievements

        //load achievement bools

        finalHordeWin = loadedAchBools[0];
        noDashAchieved = loadedAchBools[1];
        noRedLaserAchievementStatus = loadedAchBools[2];
        noRedLaserFrakinAchievementStatus = loadedAchBools[3];
        tutorialCompleted = loadedAchBools[4];
        teethOver9000ach = loadedAchBools[5];
        allStoryHeartsAch = loadedAchBools[6];
        allStoryLasersFiredOnceAch = loadedAchBools[7];
        firstProgenVictory = loadedAchBools[8];
        firstStoryHeartPiece = loadedAchBools[9];
        firstSandDollar = loadedAchBools[10];
        firstBankUse = loadedAchBools[11];
        maxBounceKills = loadedAchBools[12];
        shottyTripleKill = loadedAchBools[13];
        senseiNinjaKill = loadedAchBools[14];
        firstFivePiece = loadedAchBools[15];
        firstProgenHealthIncrease = loadedAchBools[16];
        firstHordeWin = loadedAchBools[17];
        kawaiiVictory = loadedAchBools[18];
        hundMiniBerthaKills = loadedAchBools[19];
        hundTeethGunKills = loadedAchBools[20];
        hundPewPooKills = loadedAchBools[21];
        hundSharkRepellentKills = loadedAchBools[22];
        hundFlamethrowerKills = loadedAchBools[23];
        fullUpgradeProgen = loadedAchBools[24];
        allBankSlotsBought = loadedAchBools[25];
        thousandSandDollars = loadedAchBools[26];
        killLootGoblin = loadedAchBools[27];
        blazeFishKill = loadedAchBools[28];
        killMommaSharkFirst = loadedAchBools[29];
        killAnglerFish = loadedAchBools[30];
        firstItemReroll = loadedAchBools[31];
        fullyUpgradeItem = loadedAchBools[32];
        frakinDefeated = loadedAchBools[33];
        perfectNumber = loadedAchBools[34];

        kawaiiMode = loadedAchBools[35];

        //load achievement ints      
        lootGoblinDeaths = loadedAchInts[8];
        miniBerthaKills = loadedAchInts[9];
        teethGunKills = loadedAchInts[10];
        pooPewKills = loadedAchInts[11];
        sharkRepellentKills = loadedAchInts[12];
        flamethrowerKills = loadedAchInts[13];
        platTrueInt = loadedAchInts[14];

        #endregion

        if (saveAnimatorHolder == null) return;
        _saveAnimator.Play("autoSave");
        Debug.Log("you saved your game");
    }

    //This needs to be updated after also
    public void ResetGameData()
    {
        heartPieces = 0;
        teethTotal = 0;
        proGenHealth = 6;
        permStats[(int) GameEnums.PermanentStats.FireTimeAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.WeaponDamageAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.WeaponRangeAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.MaxAmmoAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.MoveAdjustSpeed] = 1;
        permStats[(int) GameEnums.PermanentStats.DashForceAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.LinearDragAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.ProjSpeedAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.AngularDragAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.DuckHealAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.ReceivedDamageAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.InvulnFrameAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.ShadowShotAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.ReloadCooldownAdjust] = 1;
        permStats[(int) GameEnums.PermanentStats.DropRateAdjust] = 1;
        PlayerManager.instance.WeaponManagement.UpdateWeaponIndex(0);
        for (int i = 0; i < numPurchased.Length; i++)
        {
            numPurchased[i] = 0;
        }

        normalLevelArray.SetAll(false);
        heartCollectables_FrikinsGym.SetAll(false);
        kawaiiLevelArray.SetAll(false);
        achievementUnlockArray = new bool[(int) Achievements.ACH_PERFECT_NUMBER_PLUS_ONE + 1];
        lootGoblinDeaths = 0;
        miniBerthaKills = 0;
        teethGunKills = 0;
        pooPewKills = 0;
        sharkRepellentKills = 0;
        flamethrowerKills = 0;
        platTrueInt = 0;
        bank.bankedItemA = -1;
        bank.bankedItemB = -1;
        bank.bankedItemC = -1;
        bank.bankedItemD = -1;
        bank.bankedItemE = -1;
    }
    
    public void PresetBuildSwitch()
    {
        switch (devPreset)
        {
            case Presets.TestingBase:
                break;
            case Presets.FreshStart:
                PresetBuildSwitchSet(1,8,0, 0 , 1);
                break;
            case Presets.BossRush:
                PresetBuildSwitchSet(1,12, 0, 0, 8);
                break;
            case Presets.World1_Unlock:
                PresetBuildSwitchSet(1, 9, 0, 0, 2);
                break;
            case Presets.World2_Unlock:
                PresetBuildSwitchSet(levelsUnlocked = (int) LevelNames_Normal.W2Boss + 1);
                break;
            case Presets.World3_Unlock:
                PresetBuildSwitchSet(levelsUnlocked = (int) LevelNames_Normal.W3Boss + 1);
                break;
        }
    }

    private void PresetBuildSwitchSet(
        int _levelsUnlocked = 1,
        int _storyHealth = 8,
        int _heartPieces = 0,
        int _teethTotal = 0,
        int _storyLaserIndex = 8)
    {
        levelsUnlocked = _levelsUnlocked;
        storyHealth = _storyHealth;
        heartPieces = _heartPieces;
        teethTotal = _teethTotal;
        storyLasersIndex = _storyLaserIndex;
    }
    */

        // replacement in AI helper Functions
    }

    [Serializable]
    public class UpgradeCosts
    {
        public int[] upgradeCost;
    }
}