using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class ProjectileSpawnHolder : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPosition;

        public Transform[] GetSpawns() => spawnPosition;
    }
}


