using System;

namespace FrikinCore.AI.Combat.Projectiles
{
    [Serializable]
    public class Projectile
    {
        public ProjectileInfo info;

        public bool hasShrapnel, hasRotation, hasStatus;

        [DrawIf("hasStatus", true)] public StatusType statusType;

        [DrawIf("hasShrapnel", true)] public ShrapnelType shrapnelType;
        [DrawIf("hasShrapnel", true)] public float shrapnelRadius;
        [DrawIf("hasShrapnel", true)] public int numOfShrapnelProjs;
    }
}