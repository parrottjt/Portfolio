using UnityEngine;

namespace FrikinCore.Interfaces
{
    public interface IDoDamage
    {
        void DoDamage(Collider2D objectCollider2D = null);
    }
}