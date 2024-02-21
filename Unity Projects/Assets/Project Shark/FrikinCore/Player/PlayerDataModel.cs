using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Player.Combat;
using FrikinCore.Player.Health;
using FrikinCore.Player.Movement;
using FrikinCore.Stats;
using TSCore.ScriptableObject;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FrikinCore.Player
{
    public class PlayerDataModel : MonoBehaviour
    {
        public struct DefaultPlayerValues
        {
            public float GravityScale { get; }
            public float AgainTime { get; }

            public DefaultPlayerValues(float gravityScale, float againTime)
            {
                GravityScale = gravityScale;
                AgainTime = againTime;
            }
        }

        [Header("Health")]
        [SerializeField] int _startingHealth = 6;
        [SerializeField] int _startingArmor = 0;
        
        [Header("Speed")] [SerializeField] FloatVariable _playerMovementSpeed;
        [SerializeField] FloatVariable _playerDashSpeed;

        [Header("Particle Systems")] [SerializeField]
        ParticleSystem _addHealthParticleSystem;

        [SerializeField] ParticleSystem _addAmmoParticleSystem;
        [SerializeField] ParticleSystem _poisonedParticleSystem;
        [SerializeField] ParticleSystem _frozenParticleSystem;
        [SerializeField] ParticleSystem _primaryUtilityParticleSystem;
        [SerializeField] ParticleSystem _secondaryUtilityParticleSystem;

        [Header("Sprite Renderers")] [SerializeField]
        SpriteRenderer _spriteRenderer;

        [SerializeField] SpriteRenderer _weaponHolderSpriteRenderer;
        [SerializeField] SpriteRenderer _stunSpriteRenderer;
        [SerializeField] SpriteRenderer _freezeSpriteRenderer;

        [Header("Ticks")] [SerializeField] TicksInSeconds _dashTime;
        [SerializeField] TicksInSeconds _againTime;
        [SerializeField] TicksInSeconds _perfectTime;
        [SerializeField] TicksInSeconds _stunTime;
        [SerializeField] TicksInSeconds _freezeTime;
        [SerializeField] TicksInSeconds _respawnTime;

        [Header("Other Components")] [SerializeField]
        Animator _animator;

        [SerializeField] GameObject _moveReminder;

        [FormerlySerializedAs("_kawaiiMode")] [SerializeField]
        SpriteRenderer _kawaiiModeSpriteRenderer;

        [Header("Events")] [SerializeField] UnityEvent _onDestructibleHit;

        public readonly PlayerMoveSpeed PlayerMoveSpeed = new ();

        // Particle System
        public PlayerParticleSystem AddHealth { get; private set; }
        public PlayerParticleSystem AddAmmo { get; private set; }
        public PlayerParticleSystem Poison { get; private set; }
        public PlayerParticleSystem Freezing { get; private set; }
        public PlayerParticleSystem PrimaryUtilityParticleSystem { get; private set; }
        public PlayerParticleSystem SecondaryUtilityParticleSystem { get; private set; }

        // Float
        public float Speed => PlayerMoveSpeed.Speed.GetStatValue(_playerMovementSpeed.Value)
                              * TimeManager.GetLayerSpeed(TimeLayers.Player);

        public float DashSpeed => _playerDashSpeed.Value;
        public float PerfectDodgeTime => _perfectTime != null ? _perfectTime.Value : 3;

        public float ShadowShotAdjust =>
            UpdatedStatManager.GetStat(GameEnums.PermanentStats.ShadowShotAdjust).GetStatValue(1) - 1;

        public float RotationAngle { get; set; }
        public float AgainTme => _againTime.Value;
        public float DashTime => _dashTime.Value;
        public float StunTime => _stunTime.Value;
        public float FreezeTime => _freezeTime.Value;
        public float RespawnTime => _respawnTime.Value;

        // Int
        public int DirectionIndex { get; set; }

        // Bool
        public bool PerfectDodge { get; set; }

        public bool CanTakeAmbientDamage => IsStunned || InputManager.instance.WeaponWheelActiveInput() == false &&
            Conditions.AllConditionsMatchValue(false, IsMoving, IsDead, InCurrent, GracePeriodActive);

        public bool IsDead { get; set; }
        public bool IsInvulnerable { get; set; }
        public bool IsMoving => Mathf.Abs(MovementVector.x) > 0 || Mathf.Abs(MovementVector.y) > 0;
        public bool PrimaryUtilityActive { get; set; }
        public bool SecondaryUtilityActive { get; set; }
        public bool IsSlowed { get; set; }
        public bool IsOiled { get; set; } //todo: this needs to be just in UI
        public bool IsMiniOiled { get; set; } //todo: this needs to be just in UI
        public bool IsStunned { get; set; }
        public bool IsFreezing { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsPoisoned { get; set; }
        public bool InCurrent { get; set; }
        public bool GracePeriodActive { get; set; }
        public bool IsFacingRight { get; set; }
        public bool ControllerRightStickReset { get; set; }

        //todo: Find out if these are needed
        public bool HookRight { get; set; }
        public bool HookLeft { get; set; }
        public bool HookUp { get; set; }
        public bool HookDown { get; set; }

        // Vector2
        public Vector2 MovementVector { get; set; }
        public Vector2 LookVector { get; set; }

        //Sprite Renderers
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteRenderer WeaponHolderSpriteRenderer => _weaponHolderSpriteRenderer;
        public SpriteRenderer StunSpriteRenderer => _stunSpriteRenderer;
        public SpriteRenderer FreezeSpriteRenderer => _freezeSpriteRenderer;

        // todo: these might be ok to place in the scripts they belong to 
        // Animator Hash
        public static int XAxisHash => Animator.StringToHash("X-Axis");
        public static int YAxisHash => Animator.StringToHash("Y-Axis");
        public static int DirectionIndexFloatHash => Animator.StringToHash("DirectionIndexFloat");
        public static int IsMovingHash => Animator.StringToHash("isMoving");
        public static int IsStunnedHash => Animator.StringToHash("isStunned");
        public static int IsOiledHash => Animator.StringToHash("isOiled");
        public static int IsDeadHash => Animator.StringToHash("isDead");
        public static int LaserAnimBool => Animator.StringToHash("laserAnimBool");
        public static int IsMiniOilHash => Animator.StringToHash("isMiniOil");
        public static int IsRespawningHash => Animator.StringToHash("isRespawning");
        public static int DeathFadeIn => Animator.StringToHash("DeathFadeIn");

        //Other Components
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator => _animator;
        public Transform Transform { get; private set; }
        public GameObject Player { get; private set; }
        public DefaultPlayerValues DefaultValues { get; private set; }
        public UnityEvent OnDestructibleHit => _onDestructibleHit;
        public GameObject MoveReminder => _moveReminder;
        public SpriteRenderer KawaiiModeSpriteRenderer => _kawaiiModeSpriteRenderer;
        public PlayerHealth PlayerHealth { get; private set; }
        
        //Scripts
        public FiringController FiringController { get; private set; }
        public GracePeriod GracePeriod { get; private set; }
        public PlayerInvulnerability PlayerInvulnerability { get; private set; }

        void Awake()
        {
            AddHealth = new PlayerParticleSystem(_addHealthParticleSystem);
            AddAmmo = new PlayerParticleSystem(_addAmmoParticleSystem);
            Poison = new PlayerParticleSystem(_poisonedParticleSystem);
            Freezing = new PlayerParticleSystem(_frozenParticleSystem);
            PrimaryUtilityParticleSystem = new PlayerParticleSystem(_primaryUtilityParticleSystem);
            SecondaryUtilityParticleSystem = new PlayerParticleSystem(_secondaryUtilityParticleSystem);

            Rigidbody = GetComponent<Rigidbody2D>();
            FiringController = GetComponent<FiringController>();
            GracePeriod = GetComponent<GracePeriod>();
            PlayerInvulnerability = GetComponent<PlayerInvulnerability>();
            PlayerHealth = GetComponent<PlayerHealth>();
            
            Player = gameObject;
            Transform = transform;
            DefaultValues = new DefaultPlayerValues(Rigidbody.gravityScale, _againTime.Value);
        }
    }
}