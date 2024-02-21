using System.Collections.Generic;
using FrikinCore.Enumeration;
using FrikinCore.Save;
using TSCore.Utils;

namespace FrikinCore.Stats
{
    public class UpdatedStatManager : Singleton<UpdatedStatManager>
    {
        static readonly Dictionary<GameEnums.PermanentStats, Stat> statDictionary = new ();

        static readonly Dictionary<GameEnums.StatModifiers, StatModifier> statModifiersDictionary = new ();

        static readonly Dictionary<GameEnums.PermanentStats, float> statAdjustmentDictionary = new ()
            {
                { GameEnums.PermanentStats.WeaponDamageAdjust, .1f },
                { GameEnums.PermanentStats.WeaponRangeAdjust, .5f },
                { GameEnums.PermanentStats.MaxAmmoAdjust, .1f },
                { GameEnums.PermanentStats.InvulnarablityFramesAdjust, .5f },
                { GameEnums.PermanentStats.ShadowShotAdjust, .5f },
                { GameEnums.PermanentStats.ReloadCooldownAdjust, .3f },
                { GameEnums.PermanentStats.DropRateAdjust, .5f },
            };

        public static Stat GetStat(GameEnums.PermanentStats statName)
        {
            if (!statDictionary.ContainsKey(statName)) AddStatDictionaryItem(statName);
            return statDictionary[statName];
        }

        static void AddStatDictionaryItem(GameEnums.PermanentStats statName)
        {
            statDictionary.Add(statName, new Stat());
        }

        StatModifier story_WeaponDamageUpgrade;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            CreateStatModifiers();
            AddStoryUpgradeModifers();
        }

        void CreateStatModifiers()
        {
            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_WeaponDamageUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.WeaponDamageAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.WeaponDamageAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_WeaponRangeUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.WeaponRangeAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.WeaponRangeAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_MaxAmmoUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.MaxAmmoAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.MaxAmmoAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_IframesUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.InvulnarablityFramesAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.InvulnarablityFramesAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_ShadowShotUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.ShadowShotAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.ShadowShotAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_WeaponReloadTimeUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.ReloadCooldownAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.ReloadCooldownAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.Story_DropRateUpgrade,
                new StatModifier(PersistentDataManager.DataIntArrayDictionary[
                                         PersistentDataManager.DataIntArrays.NumberOfUpgradesPurchased]
                                     [(int)GameEnums.PermanentStats.DropRateAdjust] *
                                 statAdjustmentDictionary[GameEnums.PermanentStats.DropRateAdjust],
                    StatModifier.StatModType.Percent_Additive));

            statModifiersDictionary.Add(GameEnums.StatModifiers.PerfectDodge,
                new StatModifier(.5f, StatModifier.StatModType.Percent_Multiply));
        }

        void AddStoryUpgradeModifers()
        {
            GetStat(GameEnums.PermanentStats.WeaponDamageAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_WeaponDamageUpgrade]);
            GetStat(GameEnums.PermanentStats.WeaponRangeAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_WeaponRangeUpgrade]);
            GetStat(GameEnums.PermanentStats.MaxAmmoAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_MaxAmmoUpgrade]);
            GetStat(GameEnums.PermanentStats.InvulnarablityFramesAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_IframesUpgrade]);
            GetStat(GameEnums.PermanentStats.ShadowShotAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_ShadowShotUpgrade]);
            GetStat(GameEnums.PermanentStats.ReloadCooldownAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_WeaponReloadTimeUpgrade]);
            GetStat(GameEnums.PermanentStats.DropRateAdjust)
                .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.Story_DropRateUpgrade]);
        }

        public void AddPerfectDodgeModifiers() => GetStat(GameEnums.PermanentStats.WeaponDamageAdjust)
            .AddModifier(statModifiersDictionary[GameEnums.StatModifiers.PerfectDodge]);

        public void RemovePerfectDodgeModifiers() => GetStat(GameEnums.PermanentStats.WeaponDamageAdjust)
            .RemoveModifier(statModifiersDictionary[GameEnums.StatModifiers.PerfectDodge]);
    }
}