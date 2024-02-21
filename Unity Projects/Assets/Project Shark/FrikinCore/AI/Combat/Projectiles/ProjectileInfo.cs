using System;
using System.Linq;
using Sirenix.OdinInspector;
using TSCore.ScriptableObject;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public enum AttackType
    {
        NormalAttack = 1,
        StatusAttack = 0,
        HeavyAttack = 2
    }

    [CreateAssetMenu(menuName = "Game Data/Projectile Info", fileName = "Projectile Info")]
    [Serializable]
    public class ProjectileInfo : ScriptableObject
    {
        [Header("Legacy Code")] public GameObject createdProjectile;

        [DrawIf("shapedType", ShapedType.Expanding)]
        public GameObject projForShape;


        public int destroyMaxTick = 10;
        public float projSpeed = 10;

        [DrawIf("createdFunction", ProjectileFunction.Tracking)]
        public int trackingMaxTick = 10;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [Serializable]
        public struct MineProjectileInfo
        {
            public ProjectileInfo ProjectileInfo;
            public int NumberOfProjectiles;
            public float MineRadius;
        }

        public const string PREFABFOLDERPATH = "Assets/Prefabs/Enemies/Enemy Projectiles/Updated Projectile Prefabs";
        [HideInInspector] public bool _spawnsAdditionalProjectiles;

        [Header("Info Needed")] [ReadOnly] [SerializeField]
        GameObject _projectilePrefab;

        public ProjectileFunction createdFunction;

        [DrawIf("createdFunction", ProjectileFunction.Shaped)]
        public ShapedType shapedType;

        [SerializeField] AttackType _attackType = AttackType.NormalAttack;

        [DrawIf("_attackType", AttackType.StatusAttack)] [SerializeField]
        StatusType _statusType = StatusType.None;

        [SerializeField] FloatVariable _projectileSpeed;

        [DrawIf("createdFunction", ProjectileFunction.Tracking)] [SerializeField]
        TicksInSeconds _trackingMaxTick;

        [SerializeField] bool _hasShrapnel;

        [DrawIf("_hasShrapnel", true)] [SerializeField]
        ProjectileInfo _shrapnelInfo;

        [DrawIf("_hasShrapnel", true)] [SerializeField]
        ShrapnelType _shrapnelType;

        [DrawIf("_hasShrapnel", true)] [SerializeField]
        bool _hasRotation;

        [DrawIf("_hasShrapnel", true)] [SerializeField]
        float _shrapnelRadius;

        [DrawIf("_hasShrapnel", true)] [SerializeField]
        int _numberOfShrapnelProjectiles;

        [DrawIf("_spawnsAdditionalProjectiles", true)] [SerializeField]
        TicksInSeconds _destroyMaxTick;

        [DrawIf("createdFunction", ProjectileFunction.Mine)] [SerializeField]
        MineProjectileInfo[] _mineProjectileInfos;

        public GameObject ProjectilePrefab => _projectilePrefab;
        public AttackType AttackType => _attackType;
        public StatusType StatusType => _statusType;
        public ShrapnelType ShrapnelType => _shrapnelType;
        public TicksInSeconds DestroyMaxTick => _destroyMaxTick;
        public FloatVariable ProjectileSpeed => _projectileSpeed;
        public TicksInSeconds TrackingMaxTick => _trackingMaxTick;
        public bool HasShrapnel => _hasShrapnel;
        public bool HasRotation => _hasRotation;
        public float ShrapnelRadius => _shrapnelRadius;
        public int NumberOfShrapnelProjectiles => _numberOfShrapnelProjectiles;
        public ProjectileInfo ShrapnelInfo => _shrapnelInfo;
        public MineProjectileInfo[] MineProjectileInfos => _mineProjectileInfos;

        ProjectileCreation _projectileCreation;

        public ProjectileInfo()
        {
            _projectileCreation = new ProjectileCreation(this);
        }

        void OnValidate()
        {
            if (_attackType != AttackType.StatusAttack) _statusType = StatusType.None;

            if (createdFunction != ProjectileFunction.Shaped) shapedType = ShapedType.Image;

            _spawnsAdditionalProjectiles = _hasShrapnel || createdFunction == ProjectileFunction.Mine;
        }

        public void ConstructProjectile()
        {
            _projectileCreation.Creation();
        }

        public void DeconstructProjectile()
        {
            var attachedObjects = ProjectilePrefab.GetComponent<HoldAttachedProjectileInfos>();
            if (attachedObjects == null)
            {
                _projectileCreation.Destruction();
                DebugScript.Log_QuickTest(this);
                return;
            }

            attachedObjects.RemoveAttachedObject(this);

            if (attachedObjects.HasOtherAttachedObjects() == false)
            {
                _projectileCreation.Destruction();
            }
            else
            {
                _projectilePrefab = null;
            }
        }

        public void ReconstructProjectile()
        {
            DeconstructProjectile();
            ConstructProjectile();
        }

        public void CreatePrefabObject()
        {
            _projectilePrefab = PrefabUtils.CreatePrefabFromBase(PREFABFOLDERPATH,
                NameOfPrefab(), Resources.Load<GameObject>("BaseProjectilePrefab"));
        }

        public string NameOfPrefab()
        {
            string shrapnelString = "";
            string mineString = "";
            string trackingString = "";
            string statusString = "";

            if (_hasShrapnel)
            {
                shrapnelString = ShrapnelString();
            }

            if (createdFunction == ProjectileFunction.Mine)
            {
                mineString = $"_{MineProjectileInfoNamesAsString()}{DestroyMaxTick.name}";
            }

            if (createdFunction == ProjectileFunction.Tracking)
            {
                trackingString = $"_TT_{TrackingMaxTick.name}";
            }

            if (_attackType == AttackType.StatusAttack)
            {
                statusString = $"_{_statusType.ToString()}";
            }

            return $"{Enums.ShortenNameInitials(_attackType)}_{Enums.ShortenNameInitials(createdFunction)}" +
                   $"{statusString}_{Enums.ShortenNameInitials(ProjectileSpeed.name)}" +
                   $"{shrapnelString}{mineString}{trackingString}";
        }

        string ShrapnelString()
        {
            string shrapnelString;
            shrapnelString =
                $"_Shrap_{Enums.ShortenNameInitials(ShrapnelInfo.name)}_{Enums.ShortenNameInitials(ShrapnelType.ToString())}_Num" +
                $"{NumberOfShrapnelProjectiles.ToString()}" + $"_R{ShrapnelRadius}_DM" +
                $"{DestroyMaxTick.name}{(_hasRotation ? "_R" : "")}";
            return shrapnelString;
        }

        string MineProjectileInfoNamesAsString()
        {
            string mineProjectileInfoNamesAsString = MineProjectileInfos.Aggregate("",
                (current, info) => current +
                                   $"Mine_{info.ProjectileInfo.name}_" +
                                   $"Num{info.NumberOfProjectiles}_" +
                                   $"R{info.MineRadius}_");
            return mineProjectileInfoNamesAsString;
        }
    }
}