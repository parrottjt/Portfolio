using System;
using FrikinCore.Enumeration;
using FrikinCore.Interfaces;
using FrikinCore.Save;
using FrikinCore.Sfx;
using FrikinCore.Stats;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.Events;
using Math = System.Math;

namespace FrikinCore.Player.Health
{
    public class PlayerHealthController : MonoBehaviour, ITakeDamage<int>, IHaveHealthBar<int>
    {
        public class PlayerDeathArgs : EventArgs
        {
        }

        public static event EventHandler<PlayerDeathArgs> CallOnPlayerDeath;

        public static event Action OnHealthChange, OnMaxHealthChange, OnDamageTaken;

        [Header("Player Health Variables")] [SerializeField]
        int _health = 8;

        [SerializeField] int maxHealth = 8;

        //ProGen Variables
        [Header("ProGen Variables")] public float invulnFrameTime, invulnAdjust;

        bool isHealthFull;

        //Floats
        int oldMaxHealth;

        //Ints
        [HideInInspector] public int hitCountTut;

        //Invuln Variables
        float origInvulnTime = 0.7f;
        bool isInvuln, isSaving;

        //Not deleting yet

        #region Heartbar and Lives Info

        Animator heartAnim;
        static readonly int IsRespawning = Animator.StringToHash("isRespawning");

        #endregion

        [SerializeField] UnityEvent OnPlayerDeath;

        //Props
        public int Health
        {
            get => _health;
            private set
            {
                _health = (int)Mathf.Clamp(value, 0, MaxHealth);
                OnHealthChange?.Invoke();
            }
        }

        public int MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                OnMaxHealthChange?.Invoke();
            }
        }

        void OnDestroy()
        {
            PersistentDataManager.OnPresetChange -= OnPresetChange;
        }

        private void Awake() => invulnFrameTime = origInvulnTime;

        private void Start()
        {
            PersistentDataManager.OnPresetChange += OnPresetChange;
        }

        private void Update()
        {
            if (GameManager.GameStatesDictionary[GameStates.Menu]) return;


            #region Health

            PlayerManager.instance.Player.SpriteRenderer.enabled = true;


            if (GameManager.GameStatesDictionary[GameStates.Story])
            {
                MaxHealth = PersistentDataManager.DataIntDictionary[
                    PersistentDataManager.DataInts.StoryPlayerMaxHealth];
            }

            if (GameManager.instance.inProGen)
            {
                MaxHealth = PersistentDataManager.DataIntDictionary[
                    PersistentDataManager.DataInts.ProgenPlayerMaxHealth];
            }

            if (Math.Abs(oldMaxHealth - MaxHealth) > 0)
            {
                SetHealthToMax();
                oldMaxHealth = MaxHealth;
            }

            #endregion
        }

        public void PlayDamageSoundNoMove()
        {
            SoundManager.instance.RandomizeSfx(SoundManager.instance.StaticDamageSfx);
        }

        #region Health Functions

        public void AddHealth(int amount)
        {
            Health += amount;
        }

        public void RemoveHealth(int amount)
        {
            if (!(Time.timeScale > 0)) return;
            if (!isInvuln && Health > 0)
            {
                hitCountTut++;

                amount = Mathf.Clamp(amount, 0, 2);

                Health -= amount;

                isInvuln = true;

                DebugScript.Log_QuickTest(this);

                PlayerManager.instance.Player.Animator.SetBool(PlayerDataModel.IsRespawningHash, true);

                SoundManager.instance.RandomizeSfx(SoundManager.instance.PlayerDamagedSfx,
                    SoundManager.instance.PlayerDamaged2Sfx);

                Invoke(nameof(ResetInvulnerability), 1f);

                OnDamageTaken?.Invoke();

                if (Health <= 0)
                {
                    SoundManager.instance.RandomizeSfx(SoundManager.instance.DeathSoundSfx);
                    PlayerManager.instance.Player.PlayerMoveSpeed.ActivateStatusEffect(GameEnums.PlayerMovementEffects
                        .Death);
                    Debug.Log("[Player Health Controller] Call on player death args is next line");
                    CallOnPlayerDeath?.Invoke(this, new PlayerDeathArgs());
                    OnPlayerDeath?.Invoke();
                    Debug.Log("[Player Health Controller] Call on player death passed");
                }
            }
        }

        //This fixes redundent calls
        public void RemoveHealth(float amount) => RemoveHealth((int)amount);

        public float GetHealth()
        {
            return Health;
        }

        public void SetHealth(int amount)
        {
            Health = amount;
        }

        public void SetHealthToMax() => SetHealth(MaxHealth);

        public void SetMaxHealth(int amount)
        {
            MaxHealth = amount;
            if (GameManager.GameStatesDictionary[GameStates.Story])
            {
                PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryPlayerMaxHealth] =
                    (int)maxHealth;
            }
        }

        public void AddMaxHealth(int amount)
        {
            MaxHealth += amount;
            if (GameManager.GameStatesDictionary[GameStates.Story])
            {
                PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryPlayerMaxHealth] =
                    (int)maxHealth;
            }
        }

        public void RemoveMaxHealth(int amount)
        {
            MaxHealth -= amount;
            if (GameManager.GameStatesDictionary[GameStates.Story])
            {
                PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryPlayerMaxHealth] =
                    (int)maxHealth;
            }
        }

        public bool GetIsHealthFull()
        {
            return Health >= MaxHealth;
        }

        public bool IsPlayerHealthLow()
        {
            return Health <= 2 && Health > 0;
        }

        #endregion

        #region Invuln Functions

        public void ResetInvulnerability()
        {
            Physics2D.IgnoreLayerCollision(12, 14, false);
            isInvuln = false;

            PlayerManager.instance.Player.Animator.SetBool(IsRespawning, false);
        }

        public void TurnInvulnerable() //used while dashing
        {
            isInvuln = true;
            Physics2D.IgnoreLayerCollision(12, 14, true);

            var invulnTime = UpdatedStatManager.GetStat(GameEnums.PermanentStats.InvulnarablityFramesAdjust)
                .GetStatValue(1);
            Invoke(nameof(ResetInvulnerability), invulnTime);
        }

        public void SpawnInvulnerability()
        {
            isInvuln = true;
            //Wehking_PlayerSavedStats.instance.Save();
            Physics2D.IgnoreLayerCollision(12, 14, true);
        }

        #endregion

        public void TakeDamage(int damage, bool cutThroughArmor = false) => Health -= Mathf.Abs(damage);

        //Need this
        public void UpdateHealthBarValues()
        {
            throw new NotImplementedException();
        }

        void OnPresetChange()
        {
            MaxHealth = PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.StoryPlayerMaxHealth];
            SetHealthToMax();
        }
    }
}