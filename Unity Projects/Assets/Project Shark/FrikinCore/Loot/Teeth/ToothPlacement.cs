using System.Collections.Generic;
using FrikinCore.Collectable;
using FrikinCore.Enumeration;
using FrikinCore.Utils;
using TSCore;
using UnityEngine;

namespace FrikinCore.Loot.Teeth
{
    public class ToothPlacement : MonoBehaviour
    {
        static GameObject toothPrefab, teethPrefab;

        static List<string> tileMapToothNames = new List<string>
        {
            "GemBrush_Tooth",
            "GemBrush_Teeth"
        };

        static List<GameObject> currentLevelTeethList = new List<GameObject>();

        static ToothType[] GrabTileMapTeeth() => FindObjectsOfType<ToothType>();

        static void GameObjectAssignment()
        {
            toothPrefab = CollectableResourceList.GetGameObject(GameEnums.GameCollectables.Tooth);
            teethPrefab = CollectableResourceList.GetGameObject(GameEnums.GameCollectables.Teeth);
        }

        static Vector2[] GrabTileMapTeethPositions(ToothType[] _tileMapTeeth)
        {
            var tileMapTeeth = _tileMapTeeth;
            List<Vector2> tileMapTeethList = new List<Vector2>();

            foreach (var tileMapTooth in tileMapTeeth)
            {
                tileMapTeethList.Add(tileMapTooth.transform.position);
            }

            return tileMapTeethList.ToArray();
        }

        public static void PlaceCorrectToothTypeWhereTileMapPlacementIs()
        {
            var tileMapTeeth = GrabTileMapTeeth();
            var tileMapTeethPositions = GrabTileMapTeethPositions(tileMapTeeth);
            GameObjectAssignment();

            //Debug_TileMapTeethGameObjectNames();

            for (int i = 0; i < tileMapTeethPositions.Length; i++)
            {
                var toothPickUp = ObjectPooling.GetAvailable(
                    tileMapTeeth[i].TypeOfTooth == ToothPickTypes.Tooth ? toothPrefab : teethPrefab);

                currentLevelTeethList.Add(toothPickUp);

                toothPickUp.transform.position = tileMapTeethPositions[i];
                toothPickUp.SetActive(true);

                tileMapTeeth[i].TurnOffGemBrush();
            }
        }

        public static void DeactivateCurrentLevelTeeth()
        {
            Handlers.SetActiveOnGameObjectsTo(false, currentLevelTeethList.ToArray());
            currentLevelTeethList.Clear();
        }

        //Debug Checks
        static void Debug_TileMapTeethGameObjectNames()
        {
            var tileMapTeeth = GrabTileMapTeeth();
            foreach (var tileMapTooth in tileMapTeeth)
            {
                if (!tileMapToothNames.Contains(tileMapTooth.name.Split(' ')[0]))
                    print($"[Tooth Placement] Debug_TileMapTeethGameObjectNames: {tileMapTooth.name} is not a tooth");
            }

            print(
                $"[Tooth Placement] Debug_TileMapTeethGameObjectNames: Array size for teeth is {tileMapTeeth.Length}");
        }
    }
}