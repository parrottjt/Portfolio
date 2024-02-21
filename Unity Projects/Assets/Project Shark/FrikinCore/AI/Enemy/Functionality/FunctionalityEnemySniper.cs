using FrikinCore.AI.Combat;
using FrikinCore.AI.Enemy.Combat;
using FrikinCore.AI.Enemy.Combat.AttackHandlers;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Functionality
{
    public class FunctionalityEnemySniper : FunctionalityEnemyAbstract
    {
        [SerializeField] Transform projectileSpawnHolderLeft, projectileSpawnHolderRight;
        [SerializeField] Transform[] projectileSpawnsLeft, projectileSpawnsRight;

        [Header("Sniper Laser Sight")] [SerializeField]
        LineRenderer sniperLine;

        [SerializeField] Transform sniperLineOriginLeft, sniperLineOriginRight;

        [SerializeField] Color primaryColor = Color.red, flashColor = Color.white;

        RangedWeaponAttack weaponAttack;
        Transform _currentLineHolder;
        float flashTimer;

        void Awake()
        {
            UpdateProjectileCreatorVariables(projectileSpawnHolderLeft, projectileSpawnHolderRight,
                projectileSpawnsLeft, projectileSpawnsRight);
            _currentLineHolder = sniperLineOriginLeft;
            weaponAttack = dataEnemy.weaponAttack as RangedWeaponAttack;
        }

        protected override void Start()
        {
            base.Start();
            combatState = new CombatStateSniper(dataEnemy);

            combatStateMachine = new CombatStateMachine(combatState, gameObject);
            dataEnemy.Health.DeclarePawnDeathFunctionality(combatStateMachine.StopCombat);
        }

        void Update()
        {
            UpdateProjectileCreatorVariables(projectileSpawnHolderLeft, projectileSpawnHolderRight,
                projectileSpawnsLeft, projectileSpawnsRight);
            primaryCollider.transform.position = transform.position;
            combatStateMachine.RunCombat();
            SniperLineFunctionality();
        }

        void SniperLineFunctionality()
        {
            if (combatStateMachine.GetCurrentCombatState() == CombatStateMachine.CombatStates.RunCombat)
            {
                flashTimer += TimeManager.Delta * 5;
                sniperLine.enabled = weaponAttack.ReloadTick >=
                                     weaponAttack.ReloadTickMax * .5f;


                var lineOrigin = _currentLineHolder.position;
                var rayOut = Physics2D.Raycast(lineOrigin, dataEnemy.Player.position - lineOrigin);
                Debug.DrawLine(lineOrigin, rayOut.point, Color.magenta);
                sniperLine.SetPosition(0, lineOrigin);
                sniperLine.SetPosition(1, rayOut.point);
                sniperLine.colorGradient.mode = GradientMode.Fixed;
                sniperLine.startColor = SniperColorFlash();
                sniperLine.endColor = SniperColorFlash();
            }
        }

        Color SniperColorFlash()
        {
            if (weaponAttack.ReloadTick <= weaponAttack.ReloadTickMax * .75f)
            {
                return primaryColor;
            }

            if (weaponAttack.ReloadTick > weaponAttack.ReloadTickMax * .75f &&
                weaponAttack.ReloadTick < weaponAttack.ReloadTickMax * .95f)
            {
                return Color.Lerp(primaryColor, flashColor, Mathf.PingPong(flashTimer, 1));
            }

            return Color.clear;
        }
    }
}