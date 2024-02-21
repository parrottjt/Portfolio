using System;
using System.Collections;
using System.Collections.Generic;
using TSCore;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class MineProjectile : AbstractProjectile
    {
        public MineProjectileRing[] rings;

        public struct MineProjectileRing
        {
            public Projectile proj;
            public int numOfProjs;
            public float radius;
            public ProjectileInfo projectile;
        }

        public bool hasRotation;

        public Transform holder;

        const float angleSetRot = 90f;

        public List<Transform>[] array;

        public int destroyTimeMax;
        int destroyTick;

        void Awake()
        {
            array = new List<Transform>[rings.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ReturnTransforms(rings[i].radius, rings[i].numOfProjs);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (destroyTick > destroyTimeMax)
            {
                SpawnProjectiles(array, holder);
                gameObject.SetActive(false);
            }
        }


        List<Transform> ReturnTransforms(float radius, int numberOfProjectiles)
        {
            List<Transform> create = new List<Transform>();
            numberOfProjectiles = Mathf.Clamp(numberOfProjectiles, 2, 100);
            var empty = Instantiate(
                Resources.Load(hasRotation ? "ShrapnelSpawnEmpty_Rotation" : "ShrapnelSpawnEmpty") as GameObject,
                transform.position, Quaternion.identity);
            empty.transform.parent = transform;
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfProjectiles;
                var clone = Instantiate(new GameObject(), transform.position, Quaternion.Euler(0, 0, 0));
                clone.transform.parent = empty.transform;
                clone.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                create.Add(clone.transform);
                var test = Mathf.Atan2(Mathf.Sin(angle), Mathf.Cos(angle)) * Mathf.Rad2Deg - angleSetRot;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, test);
            }

            return create;
        }

        void SpawnProjectiles(List<Transform>[] spawns, Transform holder)
        {
            for (int i = 0; i < spawns.Length; i++)
            {
                for (int j = 0; j < spawns[i].Count; j++)
                {
                    var proj = ObjectPooling.GetAvailable(rings[i].proj.info.createdProjectile);
                    proj.transform.position = spawns[i][j].position;
                    proj.transform.rotation = spawns[i][j].rotation;
                    proj.transform.parent = holder;
                }
            }
        }

        void OnEnable() => TickTimeTimer.OnTick += OnTick;

        void OnDisable() => TickTimeTimer.OnTick -= OnTick;

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args) => destroyTick++;
    }
}