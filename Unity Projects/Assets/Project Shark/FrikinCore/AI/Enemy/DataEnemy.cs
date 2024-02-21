using System.Collections.Generic;
using System.Linq;
using FrikinCore.AI.Enemy.Combat.AttackHandlers;
using FrikinCore.AI.Enemy.Functionality;
using FrikinCore.AI.Enemy.Health;
using FrikinCore.AI.Enemy.Movement;
using FrikinCore.Enumeration;
using FrikinCore.Loot;
using FrikinCore.Player;
using FrikinCore.ScriptableObjects;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy
{
    public class DataEnemy : MonoBehaviour
    {
        public enum WorldIntroduced
        {
            World_1,
            World_2,
            World_3,
            World_4,
            World_5,
            World_6,
            World_7
        }

        [Header("Default Components")] public GameObject pawnHolder;
        public MovementEnemyAbstract Movement;
        public HealthEnemy Health;

        public FunctionalityEnemyAbstract EnemyFunctionality;

        //public ProjectileCreator ProjectileCreator;
        public SpriteControl spriteControl;
        public Animator Animator;
        public AnimationEnemyHolder AnimationEventHolder;
        public WeaponAttack weaponAttack;

        [Header("Loot Variables")] public GameEnums.DropProbability probability;

        public LootSystem[] systems = 
        {
            new ("Normal Chance", 50, 35, .1f, 50, 25, 10, 15),
            new ("High Health Chance", 50, 35, 1, 15, 10, 5, 8),
            new ("High Tooth Chance", 15, 10, 0, 50, 35, 5, 10),
            new ("No Drop", -1, -1, -1, -1, -1, -1, -1),
            new ("No Ammo", 50, 35, .1f, 30, 25, -1, -1)
        };

        [Header("Enemy Info")] public GameEnums.TypeOfEnemy typeOfEnemy;
        [SerializeField] WorldIntroduced worldIntroduced;

        public WorldIntroduced WorldIntroducedIn => worldIntroduced;

        public Vector2 PawnStartPosition { get; private set; }
        public Transform Player { get; private set; }

        [Header("Challenges")] [SerializeField]
        List<Challenge> challengesForEnemy;

        public List<Challenge> ChallengesForEnemy => challengesForEnemy;

        public LootSystem GetCurrentLootSystemSelected()
        {
            return PlayerManager.instance.Inventory.Weapon.WeaponCount == 1
                ? systems[(int)GameEnums.DropProbability.NoAmmo]
                : systems[(int)probability];
        }

        void Awake()
        {
            PawnStartPosition = transform.position;
            Player = FindObjectOfType<GeneralSharkPlayerBehavior>().transform;
            CheckEnemyDefaultComponents(Movement, Health,
                EnemyFunctionality,
                spriteControl, Animator,
                AnimationEventHolder); //todo: add pawnHolder.gameObject once our naming conventions are better
        }

        private void CheckEnemyDefaultComponents(params Component[] gameObjectComponents)
        {
            var expectedName = gameObject.name;

            var listOfComponents = gameObjectComponents.Where(component => component != null).ToList();

            foreach (var gameObjectComponent in listOfComponents)
            {
                if (gameObjectComponent.gameObject.name != expectedName)
                {
                    DebugScript.LogEnemyInspectorError(this, expectedName, gameObjectComponent.ToString(), "purple");
                }
            }
        }
    }
}