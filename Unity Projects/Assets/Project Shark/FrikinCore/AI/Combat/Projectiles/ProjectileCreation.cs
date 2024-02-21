using System;
using System.Collections.Generic;
using TSCore.Utils;
using UnityEngine;

//todo: move enums where they need to go
public enum ProjectileFunction
{
    Straight, //*
    Spray, //*
    Mine, //* Needs to be cleaner though
    Tracking, //*
    Arch, //*
    Shaped
}

public enum ShapedType
{
    Image,
    Expanding
}

public enum ShrapnelType
{
    Circle,
    SetByHand
}

public enum StatusType
{
    None,
    Oil,
    MiniOil,
    Stun,
    MajorKnockBack,
    Freeze,
    Slow,
    Sprint
}

namespace FrikinCore.AI.Combat.Projectiles
{
    public class ProjectileCreation
    {
        readonly ProjectileInfo _projectileInfo;

        readonly List<ProjectileInfo> _projectileInfoList = new List<ProjectileInfo>();

        readonly Dictionary<ProjectileFunction, Type> _projectileFunctionality =
            new Dictionary<ProjectileFunction, Type>
            {
                { ProjectileFunction.Straight, typeof(StraightProjectile) },
                { ProjectileFunction.Mine, typeof(MineProjectile) },
                { ProjectileFunction.Tracking, typeof(TrackingProjectile) },
                { ProjectileFunction.Arch, typeof(ArchingProjectile) },
            };

        public ProjectileCreation(ProjectileInfo projectileInfo)
        {
            _projectileInfo = projectileInfo;
        }

        public void Creation()
        {
            _projectileInfoList.Clear();
            CreatePrefabsBasedOnProjectileInfo(_projectileInfo);
            ProjectileConstruction(_projectileInfo);
        }

        public void Destruction()
        {
            ProjectileDeconstruction(_projectileInfo);

            _projectileInfoList.Clear();
        }

        public void Recreation()
        {
            Destruction();
            Creation();
        }

        void ProjectileConstruction(ProjectileInfo projectileInfo)
        {
            DefaultProjectileCreation(projectileInfo);

            if (projectileInfo.HasShrapnel)
            {
                ShrapnelSetup(projectileInfo);
            }
        }

        void DefaultProjectileCreation(ProjectileInfo projectileInfo)
        {
            var projectileCreation = AddProjectileComponentToPrefab(projectileInfo);

            if (projectileCreation == null) return;

            if (!Validation.EnsurePrefabIsAttachedAndHasComponent<HoldAttachedProjectileInfos>(projectileInfo
                    .ProjectilePrefab))
            {
                projectileInfo.ProjectilePrefab.AddComponent<HoldAttachedProjectileInfos>()
                    .AddAttachedObject(projectileInfo);
            }
            else
            {
                projectileInfo.ProjectilePrefab.GetComponent<HoldAttachedProjectileInfos>()
                    .AddAttachedObject(projectileInfo);
            }

            switch (projectileInfo.createdFunction)
            {
                case ProjectileFunction.Tracking:
                    var tracking = projectileCreation as TrackingProjectile;
                    if (tracking != null) tracking.trackingMaxTime = projectileInfo.trackingMaxTick;
                    break;

                case ProjectileFunction.Mine:
                    var mineProjectile = projectileCreation as MineProjectile;
                    if (mineProjectile != null)
                    {
                        mineProjectile.destroyTimeMax = projectileInfo.destroyMaxTick;
                        mineProjectile.rings =
                            new MineProjectile.MineProjectileRing[projectileInfo.MineProjectileInfos.Length];
                        for (var i = 0; i < projectileInfo.MineProjectileInfos.Length; i++)
                        {
                            mineProjectile.rings[i].numOfProjs =
                                projectileInfo.MineProjectileInfos[i].NumberOfProjectiles;
                            mineProjectile.rings[i].projectile = projectileInfo.MineProjectileInfos[i].ProjectileInfo;
                            mineProjectile.rings[i].radius = projectileInfo.MineProjectileInfos[i].MineRadius;

                            DefaultProjectileCreation(projectileInfo.MineProjectileInfos[i].ProjectileInfo);
                        }
                    }

                    break;

                case ProjectileFunction.Arch:
                    projectileCreation.gameObject.AddComponent<ArchingPathGraph>();
                    break;
            }
        }

        void ShrapnelSetup(ProjectileInfo projectileInfo)
        {
            if (Validation.EnsurePrefabIsAttachedAndHasComponent<ShrapnelSpawn>(projectileInfo.ProjectilePrefab))
                return;

            var shrapnel = projectileInfo.ProjectilePrefab.AddComponent<ShrapnelSpawn>();
            ProjectileConstruction(projectileInfo.ShrapnelInfo);

            UpdateShrapnelVariables(projectileInfo, shrapnel);
        }

        void UpdateShrapnelVariables(ProjectileInfo projectileInfo, ShrapnelSpawn shrapnel)
        {
            shrapnel.projectileType = projectileInfo.ShrapnelInfo.ProjectilePrefab;
            shrapnel.numberOfProjectiles = projectileInfo.NumberOfShrapnelProjectiles;
            shrapnel.radius = projectileInfo.ShrapnelRadius;
            shrapnel.shrapnelType = projectileInfo.ShrapnelType;
            shrapnel.hasRotation = projectileInfo.HasRotation;
            shrapnel.disableTick = Mathf.RoundToInt(projectileInfo.DestroyMaxTick.Value);
        }

        AbstractProjectile AddProjectileComponentToPrefab(ProjectileInfo projectileInfo)
        {
            var type = _projectileFunctionality[projectileInfo.createdFunction];
            if (Validation.EnsurePrefabIsAttachedAndHasComponent<AbstractProjectile>(projectileInfo.ProjectilePrefab))
                return projectileInfo.ProjectilePrefab.GetComponent(type) as AbstractProjectile;

            if (projectileInfo.ProjectilePrefab == null) return null;
            var abstractProjectile = (AbstractProjectile)projectileInfo.ProjectilePrefab.AddComponent(type);

            abstractProjectile.speed = projectileInfo.ProjectileSpeed.Value;
            abstractProjectile.statusType = projectileInfo.StatusType;
            abstractProjectile.damage = (int)projectileInfo.AttackType;
            return abstractProjectile;
        }

        void ProjectileDeconstruction(ProjectileInfo projectileInfo)
        {
            PrefabUtils.DestroyPrefab(ProjectileInfo.PREFABFOLDERPATH, projectileInfo.ProjectilePrefab.name);
        }

        void CreatePrefabsBasedOnProjectileInfo(ProjectileInfo projectileInfo)
        {
            _projectileInfoList.Add(projectileInfo);

            //This doesn't work
            bool isCurrentListItem = _projectileInfoList[_projectileInfoList.Count - 1] == projectileInfo;
            bool hasBeenCreatedBefore = _projectileInfoList.Contains(projectileInfo);

            if (hasBeenCreatedBefore && !isCurrentListItem) return;
            projectileInfo.CreatePrefabObject();
            if (projectileInfo.HasShrapnel)
            {
                CreatePrefabsBasedOnProjectileInfo(projectileInfo.ShrapnelInfo);
            }

            if (projectileInfo.createdFunction != ProjectileFunction.Mine) return;
            foreach (var mineProjectileInfo in projectileInfo.MineProjectileInfos)
            {
                CreatePrefabsBasedOnProjectileInfo(mineProjectileInfo.ProjectileInfo);
            }
        }
    }
}