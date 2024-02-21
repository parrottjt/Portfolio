using FrikinCore.AI.Combat;
using FrikinCore.AI.Enemy.Combat;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Functionality
{
    public class FunctionalityEnemyBasicRanged : FunctionalityEnemyAbstract
    {
        [SerializeField] Transform projectileSpawnHolderLeft;
        [SerializeField] Transform projectileSpawnHolderRight;
        [SerializeField] Transform[] projectileSpawnsLeft;
        [SerializeField] Transform[] projectileSpawnsRight;

        void Awake()
        {
            UpdateProjectileCreatorVariables(projectileSpawnHolderLeft, projectileSpawnHolderRight,
                projectileSpawnsLeft, projectileSpawnsRight);
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            combatState = new CombatStateRanged(dataEnemy);

            combatStateMachine = new CombatStateMachine(combatState, gameObject);
            dataEnemy.Health.DeclarePawnDeathFunctionality(combatStateMachine.StopCombat);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateProjectileCreatorVariables(projectileSpawnHolderLeft, projectileSpawnHolderRight,
                projectileSpawnsLeft, projectileSpawnsRight);
            primaryCollider.transform.position = transform.position;
            combatStateMachine.RunCombat();
        }
    }
}