using System.Collections.Generic;
using FrikinCore.Enumeration;
using UnityEngine;

namespace FrikinCore.Collectable
{
    public class CollectableResourceList : MonoBehaviour
    {
        static Dictionary<GameEnums.GameCollectables, GameObject> gameCollectableDictionary;

        static bool isInitialized;

        public static GameObject GetGameObject(GameEnums.GameCollectables collectable)
        {
            CreateGameCollectableList();
            return gameCollectableDictionary[collectable];
        }

        public static List<GameObject> GetListOfGameObjectsOfTypeDefault<T>() where T : MonoBehaviour
        {
            CreateGameCollectableList();
            List<GameObject> list = new List<GameObject>();
            foreach (var collectableKeyValuePair in gameCollectableDictionary)
            {
                if (collectableKeyValuePair.Value.GetComponent<T>() != null)
                {
                    list.Add(collectableKeyValuePair.Value);
                }
            }

            return list;
        }

        public static List<T> GetListOfComponentsOfType<T>() where T : MonoBehaviour
        {
            CreateGameCollectableList();
            List<T> list = new List<T>();
            foreach (var collectableKeyValuePair in gameCollectableDictionary)
            {
                if (collectableKeyValuePair.Value.GetComponent<T>())
                {
                    list.Add(collectableKeyValuePair.Value.GetComponent<T>());
                }
            }

            return list;
        }

        static void CreateGameCollectableList()
        {
            if (isInitialized) return;
            gameCollectableDictionary = new Dictionary<GameEnums.GameCollectables, GameObject>
            {
                { GameEnums.GameCollectables.Tooth, Resources.Load<GameObject>("LootItems/Tooth") },
                { GameEnums.GameCollectables.Teeth, Resources.Load<GameObject>("LootItems/Teeth") },
                { GameEnums.GameCollectables.BronzeTooth, Resources.Load<GameObject>("LootItems/Bronze Tooth") },
                { GameEnums.GameCollectables.BronzeTeeth, Resources.Load<GameObject>("LootItems/Bronze Teeth") },
                { GameEnums.GameCollectables.SilverTooth, Resources.Load<GameObject>("LootItems/Silver Tooth") },
                { GameEnums.GameCollectables.SilverTeeth, Resources.Load<GameObject>("LootItems/Silver Teeth") },
                { GameEnums.GameCollectables.GoldTooth, Resources.Load<GameObject>("LootItems/Gold Tooth") },
                { GameEnums.GameCollectables.GoldTeeth, Resources.Load<GameObject>("LootItems/Gold Teeth") },
            };
            //DebugScript.Log(typeof(CollectableResourceList), $"{gameCollectableDictionary.Count}");
            isInitialized = true;
        }
    }
}