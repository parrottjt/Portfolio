using System.Collections.Generic;
using TSCore.Utils;
using UnityEngine;

namespace TSCore
{
    public static class ObjectPooling
    {
        struct ObjectPool
        {
            public Transform holder;
            public Queue<GameObject> pool;

            public ObjectPool(Transform holder)
            {
                this.holder = holder;
                pool = new Queue<GameObject>();
            }
        }

        static Dictionary<GameObject, ObjectPool> ObjectPools = new Dictionary<GameObject, ObjectPool>();

        public static Transform GetObjectHolder(GameObject prefab) => ObjectPools.ContainsKey(prefab) ? ObjectPools[prefab].holder : null;

        public static GameObject GetAvailable(GameObject prefab, bool setActiveImmediate = false)
        {
            GameObject chosenObject;
            if (ObjectPools.ContainsKey(prefab))
            {
                chosenObject = FindFirstDeactivatedObject(prefab);
            }
            else
            {
                CreateNewPool(prefab);
                chosenObject = AddObject(prefab);
            }

            chosenObject.SetActive(setActiveImmediate);
            return chosenObject;
        }

        static void CreateNewPool(GameObject prefab, int depth = 0)
        {
            var holder = new GameObject($"{prefab.name} Holder");
            Object.DontDestroyOnLoad(holder);

            var pool = new ObjectPool(holder.transform);

            ObjectPools.Add(prefab, pool);
            for (int i = 0; i < depth; i++)
            {
                AddObject(prefab);
            }
        }

        static GameObject AddObject(GameObject prefab)
        {
            var createdObject = Object.Instantiate(prefab, ObjectPools[prefab].holder.transform);
            createdObject.SetActive(false);
            createdObject.name = prefab.name;
            ObjectPools[prefab].pool.Enqueue(createdObject);
            return createdObject;
        }

        static GameObject FindFirstDeactivatedObject(GameObject prefab)
        {
            var count = 0;
            var pool = ObjectPools[prefab].pool;
            var poolCount = pool.Count;
            while (count < poolCount)
            {
                var poolObject = pool.Dequeue();
                count += 1;
                if (poolObject.IsNull()) continue;
                pool.Enqueue(poolObject);
                if (poolObject.activeSelf == false) return poolObject;
            }

            DebugScript.Log(typeof(ObjectPool), $"No inactive {prefab.name} adding a new {prefab.name}");
            return AddObject(prefab);
        }
    }
}