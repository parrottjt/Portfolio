using FrikinCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class ShapedProjectile : MonoBehaviour
    {
        public GameObject projForShape;

        public Transform[] positions;

        void Awake()
        {
            positions = GetComponentsInChildren<Transform>();
        }

        void Start()
        {
            Handlers.FireProjectile(projForShape, positions);
        }
    }
}
