using System.Collections.Generic;
using System.Linq;
using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Player;
using FrikinCore.Player.Health;
using FrikinCore.Player.Weapons;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Loot
{
    public class LootManager : MonoBehaviour
    {
        const float _SuperDuckWeight_ = 3000;

        public static readonly Dictionary<GameEnums.LevelRewards, LevelReward> levelRewards =            new ();

        readonly Dictionary<GameEnums.LevelRewards, LevelRewardValues> levelRewardValues = new ()
            {
                { GameEnums.LevelRewards.Speedrun, new LevelRewardValues(500, "Speedrun") },
                { GameEnums.LevelRewards.LowDamage, new LevelRewardValues(250, "Low Damage") },
                { GameEnums.LevelRewards.NoDamage, new LevelRewardValues(500, "No Damage") },
                { GameEnums.LevelRewards.Genicide, new LevelRewardValues(300, "Piscocide", false) },
                { GameEnums.LevelRewards.OnlyRed, new LevelRewardValues(250, "Red Laser Only") },
            };

        struct LevelRewardValues
        {
            public readonly int Value;
            public readonly string RewardName;
            public readonly bool RewardTriggerBaseValue;

            public LevelRewardValues(int value, string rewardName, bool rewardTriggerBaseValue = true)
            {
                Value = value;
                RewardName = rewardName;
                RewardTriggerBaseValue = rewardTriggerBaseValue;
            }
        }

        int _damageTaken;
        float _timeToCompleteLevel;

        public bool SuperDuckHasDropped { get; set; }

        public int EnemiesKilledAtLowHealth { get; set; }

        public float NormalDuckAdditionWeight { get; set; }
        public float PHDuckAdditionWeight { get; set; }

        public float SuperDuckAdditionWeight
        {
            get
            {
                var weight = 0f;
                if (EnemiesKilledAtLowHealth >= 5 && !SuperDuckHasDropped)
                {
                    weight = PlayerManager.instance.HealthController.IsPlayerHealthLow() ? _SuperDuckWeight_ : 0;
                }

                return weight;
            }
        }

        public float ToothAdditionWeight { get; set; }
        public float TeethAdditionWeight { get; set; }

        void Awake()
        {
            BuildLevelRewardDictionary();
        }

        void Start()
        {
            GameManager.CallOnSceneChange += OnSceneChange;
            PlayerHealthController.OnDamageTaken += OnPlayerDamageTaken;
            TickTimeTimer.OnTick += OnTick;
        }

        void OnDestroy()
        {
            GameManager.CallOnSceneChange -= OnSceneChange;
            PlayerHealthController.OnDamageTaken -= OnPlayerDamageTaken;
            TickTimeTimer.OnTick -= OnTick;
        }

        void OnPlayerDamageTaken()
        {
            if (_damageTaken < 5) _damageTaken += 1;
            if (_damageTaken == 5) levelRewards[GameEnums.LevelRewards.LowDamage].RevokeReward();
            if (_damageTaken > 0) levelRewards[GameEnums.LevelRewards.NoDamage].RevokeReward();
        }

        void OnSceneChange()
        {
            _damageTaken = 0;
            ResetLevelRewardsTrigger();
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs onTickEventArgs)
        {
            LootManagerFunctionality();
        }

        public void GenocideCheck()
        {
            if (GameManager.GameSettings[Settings.IsBossScene]) return;

            if (GameManager.instance.EnemyAliveCount == 0)
            {
                levelRewards[GameEnums.LevelRewards.Genicide].TriggerReward();
            }
        }

        public void SpeedRunCheck()
        {
            if (Time.timeSinceLevelLoad > _timeToCompleteLevel)
            {
                levelRewards[GameEnums.LevelRewards.Speedrun].RevokeReward();
            }
        }

        public void SetTimeToCompleteLevel(float timeInMinutes)
        {
            _timeToCompleteLevel = timeInMinutes * 60;
        }

        void BuildLevelRewardDictionary()
        {
            foreach (var levelReward in levelRewardValues.Keys.Where(levelReward =>
                         levelRewards.ContainsKey(levelReward) == false))
            {
                levelRewards.Add(levelReward,
                    new LevelReward(levelRewardValues[levelReward].RewardName,
                        levelRewardValues[levelReward].Value,
                        levelRewardValues[levelReward].RewardTriggerBaseValue));
            }
        }

        void LootManagerFunctionality()
        {
            if (PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponName != WeaponName.NormalLazer
                && InputManager.instance.AttackInput())
            {
                levelRewards[GameEnums.LevelRewards.OnlyRed].RevokeReward();
            }
        }

        public void ResetLevelRewardsTrigger()
        {
            foreach (var levelRewardValue in levelRewards.Values)
            {
                levelRewardValue.ResetRewardTrigger();
            }
        }
    }
}
