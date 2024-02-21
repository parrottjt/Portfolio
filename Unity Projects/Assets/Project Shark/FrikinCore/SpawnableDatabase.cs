using System.Collections.Generic;
using FrikinCore.Loot;
using UnityEngine;

namespace FrikinCore
{
    public class SpawnableDatabase : MonoBehaviour
    {
        static Dictionary<int, ISpawn> Spawnable;

        SpawnableObject[] _teeth;

        int _index = 1;
        
        void OnEnable()
        {
            Spawnable = new Dictionary<int, ISpawn>();
            CreateDictionary();
        }

        void Start()
        {
            print(Get(1));
        }

        void CreateDictionary()
        {
            _teeth = Resources.LoadAll<SpawnableObject>("Spawnable");
            foreach (var teeth in _teeth)
            {
                Add(teeth, _index, _teeth.Length);
            }
        }

        void Add(ISpawn spawnable, int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                if (Spawnable.ContainsKey(i)) continue;
                spawnable.ID = i;
                _index = i + 1;
                Spawnable.Add(i, spawnable);
            }
        }

        public static GameObject Get(int index) => Spawnable[index].Spawnable;
    }
}