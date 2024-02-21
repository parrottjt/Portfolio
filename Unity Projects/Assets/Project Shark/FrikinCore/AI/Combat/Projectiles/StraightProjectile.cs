using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class StraightProjectile : AbstractProjectile
    {
        void Update()
        {
            transform.Translate(transform.up * ModifiedSpeed(), Space.World);
        }
    }
}
