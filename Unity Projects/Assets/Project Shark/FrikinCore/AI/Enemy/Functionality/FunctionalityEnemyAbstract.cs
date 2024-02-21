using FrikinCore.AI.Combat;
using FrikinCore.AI.Enemy.Combat.AttackHandlers;
using FrikinCore.Interfaces;
using FrikinCore.Player;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Functionality
{
    public abstract class FunctionalityEnemyAbstract : MonoBehaviour, IPause
    {
        [SerializeField] protected DataEnemy dataEnemy;

        [Header("Collision Variables")] [SerializeField]
        protected float meleeForce;

        [SerializeField] protected float meleeForceRadius;

        public Collider2D primaryCollider;

        protected ICombatState combatState;
        protected CombatStateMachine combatStateMachine;

        bool _closeToPlayer;

        protected virtual void Start()
        {
            PauseManager.OnPause += OnPause;
            PauseManager.OnUnpause += OnUnpause;
        }

        protected virtual void OnDestroy()
        {
            PauseManager.OnPause -= OnPause;
            PauseManager.OnUnpause -= OnUnpause;
        }

        public bool CloseToPlayer
        {
            get { return _closeToPlayer; }
            set
            {
                if (value == _closeToPlayer || !primaryCollider.enabled) return;
                _closeToPlayer = value;
                if (_closeToPlayer)
                {
                    combatStateMachine.StartCombat();
                }
                else combatStateMachine.StopCombat();
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GameTags.Player.ToString()))
            {
                PlayerManager.instance.HealthController.RemoveHealth(1);
                var rigidBody2d = other.gameObject.GetComponent<Rigidbody2D>();
                foreach (var contactPoint2D in other.contacts)
                {
                    UtilsClass.AddExplosionForce(rigidBody2d, meleeForce, contactPoint2D.point, meleeForceRadius);
                }
            }
        }

        protected void UpdateProjectileCreatorVariables(Transform projectileSpawnHolderLeft,
            Transform projectileSpawnHolderRight, Transform[] projectileSpawnsLeft, Transform[] projectileSpawnsRight)
        {
            var rangedWeaponAttack = dataEnemy.weaponAttack as RangedWeaponAttack;
            if (dataEnemy.Movement == null || rangedWeaponAttack == null) return;

            var spawnHolder = projectileSpawnHolderLeft;
            var spawnLocations = projectileSpawnsLeft;
            if (dataEnemy.Movement.dontFlip == false)
            {
                spawnHolder = dataEnemy.Movement.facingLeft
                    ? projectileSpawnHolderLeft
                    : projectileSpawnHolderRight;
                spawnLocations = dataEnemy.Movement.facingLeft
                    ? projectileSpawnsLeft
                    : projectileSpawnsRight;
            }

            rangedWeaponAttack.UpdateSpawnLocationInformation(spawnHolder, spawnLocations);
        }

        public bool CanAttack = true;

        public virtual void OnPause()
        {
            CanAttack = false;
            DebugScript.Log(typeof(DataEnemy), "Stopping Attack!");
        }

        public virtual void OnUnpause()
        {
            CanAttack = true;
        }
    }
}