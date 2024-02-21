using System;
using FrikinCore.AI.Combat.Projectiles;
using FrikinCore.Player;
using FrikinCore.Utils;
using TSCore;
using TSCore.ScriptableObject;
using TSCore.Time;
using UnityEngine;
using Math = TSCore.Utils.Math;
using Random = UnityEngine.Random;

namespace FrikinCore.AI.Enemy.Combat.AttackHandlers
{
    public class RangedWeaponAttack : WeaponAttack
    {
        [Header("Projectile Information")] [SerializeField]
        ProjectileInfo _projectileInfo;

        [SerializeField] bool _aimAtPlayer = true;

        [Header("Projectile Spawn")] [SerializeField]
        Transform _spawnHolder;

        [SerializeField] Transform[] _spawnLocations;

        [Header("Weapon Attack Information")] [SerializeField]
        bool _usesSprayPattern;

        [DrawIf("_usesSprayPattern", true)] [SerializeField]
        FloatVariable _maxAngleAdditionForSpray;

        [SerializeField] TicksInSeconds _fireTickMax;
        [SerializeField] TicksInSeconds _reloadTickMax;

        [SerializeField] bool _ammoNeedsToStartAtZero;
        [SerializeField] int _maxAmmo;

        int _fireTick, _reloadTick;
        int _currAmmo;

        public int ReloadTick => _reloadTick;
        public float ReloadTickMax => _reloadTickMax.Value;

        void Awake()
        {
            _currAmmo = _ammoNeedsToStartAtZero ? 0 : _maxAmmo;
            if (_projectileInfo.ProjectilePrefab == null)
            {
                _projectileInfo.ConstructProjectile();
            }
        }

        bool CanReload()
        {
            return _reloadTick > _reloadTickMax.Value && HasAmmo() == false;
        }

        public bool HasAmmo()
        {
            return _currAmmo > 0;
        }

        bool CanFire()
        {
            if (HasAmmo() == false)
            {
                return false;
            }

            return _fireTick >= _fireTickMax.Value;
        }

        public override void Attack(Action methodForAnimation = null, bool additionalAttackCondition = true)
        {
            if (CanFire() && additionalAttackCondition)
            {
                foreach (var spawnLocation in _spawnLocations)
                {
                    SpawnProjectile(_projectileInfo.ProjectilePrefab, spawnLocation);
                }

                methodForAnimation?.Invoke();
                _currAmmo--;
                _fireTick = 0;
            }

            if (CanReload()) Reload();
        }

        public void AnimationProjectileFire()
        {
            Handlers.FireProjectile(_projectileInfo.ProjectilePrefab, _spawnLocations);
        }

        public void Reload()
        {
            _currAmmo = _maxAmmo;
            _reloadTick = 0;
        }

        void SpawnProjectile(GameObject projectilePrefab, Transform spawnLocation)
        {
            GameObject spawnedProjectile = ObjectPooling.GetAvailable(projectilePrefab);
            spawnedProjectile.transform.position = spawnLocation.position;
            spawnedProjectile.transform.rotation = spawnLocation.rotation;
            spawnedProjectile.SetActive(true);
        }

        protected override void OnTickFunctionality(object sender, TickTimeTimer.OnTickEventArgs e)
        {
            if (HasAmmo()) _fireTick += 1;
            else _reloadTick += 1;

            if (_aimAtPlayer) _spawnHolder.rotation = AimAtPlayer();

            if (_usesSprayPattern)
            {
                foreach (var spawnLocation in _spawnLocations)
                {
                    spawnLocation.localRotation = Quaternion.Euler(0, 0,
                        Random.Range(-_maxAngleAdditionForSpray.Value, _maxAngleAdditionForSpray.Value));
                }
            }
        }

        Quaternion AimAtPlayer()
        {
            return Math.RotationLookAtYAxis(GameManager.IsInitialized
                    ? PlayerManager.instance.Player.transform
                    : GameObject.FindWithTag(GameTags.Player.ToString()).transform,
                _spawnHolder);
        }

        public void UpdateSpawnLocationInformation(Transform holder, Transform[] spawnLocations)
        {
            _spawnHolder = holder;
            _spawnLocations = spawnLocations;
        }
    }
}