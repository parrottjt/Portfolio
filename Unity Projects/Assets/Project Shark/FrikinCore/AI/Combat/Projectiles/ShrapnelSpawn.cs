using System;
using TSCore;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class ShrapnelSpawn : MonoBehaviour
    {
        public GameObject projectileType;
        public Transform holder;
        public int numberOfProjectiles;
        public float radius = 5;
        public bool hasRotation;
        const float angleSetRot = 90;

        int tick;
        public int disableTick;

        public ShrapnelType shrapnelType;

        Action[] types;
        Action currentType;

        public bool CanFireHandSet { get; set; }

        void OnEnable()
        {
            CanFireHandSet = true;
            types = new Action[] { PlaceInCircle, PlaceAsSet };
            currentType = types[(int)shrapnelType];

            tick = 0;
        }

        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
        }

        void OnDisable() => currentType();

        void PlaceInCircle()
        {
            numberOfProjectiles = Mathf.Clamp(numberOfProjectiles, 2, 100);
            var empty = Instantiate(
                Resources.Load(hasRotation ? "ShrapnelSpawnEmpty_Rotation" : "ShrapnelSpawnEmpty") as GameObject,
                transform.position, Quaternion.identity);
            empty.transform.parent = holder;
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfProjectiles;
                var clone = ObjectPooling.GetAvailable(projectileType);
                clone.SetActive(true);
                clone.transform.parent = empty.transform;
                clone.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

                var test = Mathf.Atan2(Mathf.Sin(angle), Mathf.Cos(angle)) * Mathf.Rad2Deg - angleSetRot;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, test);
            }
        }

        void PlaceAsSet()
        {
            if (!CanFireHandSet) return;
            var empty = Instantiate(Resources.Load("ShrapnelConeSpawnEmpty") as GameObject, transform.position,
                transform.rotation);

            var spawns = empty.GetComponent<ProjectileSpawnHolder>().GetSpawns();
            foreach (var spawn in spawns)
            {
                var proj = Instantiate(projectileType, spawn.position, spawn.rotation, empty.transform);
            }
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            if (shrapnelType == ShrapnelType.SetByHand) return;
            tick += 1;
            if (tick >= disableTick) gameObject.SetActive(false);
        }
    }
}
