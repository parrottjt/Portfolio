using System.Collections.Generic;
using System.Linq;
using FrikinCore.AI.Combat.Projectiles;
using FrikinCore.Collectable;
using FrikinCore.Enumeration;
using FrikinCore.Loot.Teeth;
using FrikinCore.Score;
using TSCore;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Utils
{
    public static class Handlers
    {
        public static RaycastHit2D RaycastAgainstWall(Ray2D ray2D, float distance)
        {
            return Physics2D.Raycast(ray2D.origin, ray2D.direction, distance, Conditions.WallCheck<GameEnums.WallLayers>());
        }
        
        public static void SetActiveOnGameObjectsTo(bool value, params GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(value);
            }
        }

        public static void SetActiveOnGameObjectsTo(bool value, params IEnumerable<GameObject>[] gameObjectArrays)
        {
            foreach (var gameObjectArray in gameObjectArrays)
            {
                SetActiveOnGameObjectsTo(value, gameObjectArray);
            }
        }

        public static void SetEnableOnBehaviorComponentTo(bool value, params Behaviour[] behaviours)
        {
            foreach (var behavior in behaviours)
            {
                if (behavior != null) behavior.enabled = value;
            }
        }

        public static void SetEnableOnBehaviorArrayComponentTo(bool value, params IEnumerable<Behaviour>[] behaviors)
        {
            foreach (var behaviours in behaviors)
            {
                SetEnableOnBehaviorComponentTo(value, (Behaviour[]) behaviours);
            }
        }

        public static void SetEnableOnRendererComponentTo(bool value, params Renderer[] renderers)
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = value;
            }
        }

        public static void SetEnableOnRendererArrayComponentTo(bool value, params IEnumerable<Renderer>[] renderers)
        {
            foreach (var renderer in renderers)
            {
                SetEnableOnRendererComponentTo(value, (Renderer[]) renderer);
            }
        }
        
        public static void FireProjectile(GameObject projectile, Transform[] spawnPoint)
        {
            foreach (var t in spawnPoint)
            {
                FireProjectile(projectile, t);
            }
        }
        
        public static void FireProjectile(GameObject projectile, Transform spawnPoint)
        {
            var proj = ObjectPooling.GetAvailable(projectile);
            proj.transform.position = spawnPoint.position;
            proj.transform.rotation = spawnPoint.rotation;
            proj.SetActive(true);
        }
        
        public static void DisableProjectiles<T>() where T : Object
        {
            if (Object.FindObjectsOfType<T>() == null) return;
            foreach (var t in Object.FindObjectsOfType<T>())
            {
                if (t == null) return;
                var gameObject = t as GameObject;
                if (gameObject != null) gameObject.SetActive(false);
                var abstractProjectile = t as AbstractProjectile;
                if (abstractProjectile != null) abstractProjectile.DisableGameObject();
            }
        }

        public static void SpawnParticleSystem(GameObject particleSystem, Vector3 spawnPosition)
        {
            var system = ObjectPooling.GetAvailable(particleSystem);

            system.transform.position = spawnPosition;
            system.SetActive(true);
            system.GetComponent<ParticleSystem>().Play();
        }
        
        public enum DesiredScoreCalculationType
        {
            Default,
            RandomizeCount
        }
        public static int[] GetToothCollectablesFromScoreCalculation(int desiredScore, out List<GameObject> objectList, DesiredScoreCalculationType calculationType = DesiredScoreCalculationType.Default)
        {
            List<int> numberForEachToothCollectable;
            objectList = ToothCollectables();
            switch (calculationType)
            {
                case DesiredScoreCalculationType.RandomizeCount:
                    numberForEachToothCollectable = GetToothCollectablesRandomNumberOfTypes(objectList, desiredScore);
                    break;
                case DesiredScoreCalculationType.Default:
                default:
                    numberForEachToothCollectable = GetToothCollectablesDefaultCalculation(objectList, desiredScore);
                    break;
            }
            return numberForEachToothCollectable.ToArray();
        }

        static List<int> GetToothCollectablesDefaultCalculation(IReadOnlyList<GameObject> toothCollectables, int desiredScore)
        {
            List<int> numberForEachToothCollectable = new List<int>();

            int updatedScore = desiredScore;
            //Do math with list
            for (int i = 0; i < toothCollectables.Count; i++)
            {
                var toothValue = (int) toothCollectables[i].GetComponent<CollectableScore>().ScoreAmount;
                numberForEachToothCollectable.Add(updatedScore / toothValue);
                updatedScore -= numberForEachToothCollectable[i] * toothValue;
            }
            DebugScript.Log(typeof(Handlers), $"Using default calculation method : {numberForEachToothCollectable.Count}");
            return numberForEachToothCollectable;
        }

        static List<int> GetToothCollectablesRandomNumberOfTypes(IReadOnlyList<GameObject> toothCollectables, int desiredScore)
        {
            List<int> toothCollectableCountList = new List<int>();
            var updatedScore = desiredScore;
            
            //For loop to ensure order, order is important
            for (int i = 0; i < toothCollectables.Count; i++)
            {
                var scoreType = toothCollectables[i].GetComponent<CollectableScore>().ScoreAmount;
                var toothValue = (int)scoreType;

                var effectDivisibleScore = updatedScore / toothValue;
                var randomToothCountOfCurrentValue = 0;
                if (scoreType == ScoreHandler.ScoreValues.One)
                {
                    randomToothCountOfCurrentValue = effectDivisibleScore;
                }
                else
                {
                    if (effectDivisibleScore >= 8)
                    {
                        var minimumToothCount = effectDivisibleScore / 4;
                        var maximumToothCount = minimumToothCount * 3;

                        randomToothCountOfCurrentValue = Random.Range(minimumToothCount * 2, maximumToothCount + 1);
                    }
                }

                toothCollectableCountList.Add(randomToothCountOfCurrentValue);
                updatedScore -= randomToothCountOfCurrentValue * toothValue;
                DebugScript.Log(typeof(Handlers), $"{scoreType} Count: {randomToothCountOfCurrentValue}, Score Left: {updatedScore}");
            }
            
            return toothCollectableCountList;
        }

        static List<GameObject> ToothCollectables()
        {
            var toothCollectables = CollectableResourceList.GetListOfGameObjectsOfTypeDefault<ToothMovementForExplosion>()
                .OrderBy(item => (int)item.GetComponentInChildren<CollectableScore>().ScoreAmount)
                .Reverse().ToList();

            toothCollectables.Remove(CollectableResourceList.GetGameObject(GameEnums.GameCollectables.BronzeTeeth));
            toothCollectables.Remove(CollectableResourceList.GetGameObject(GameEnums.GameCollectables.SilverTeeth));
            toothCollectables.Remove(CollectableResourceList.GetGameObject(GameEnums.GameCollectables.GoldTeeth));
            DebugScript.Log(typeof(Handlers), $"Tooth Collectable Count: {toothCollectables.Count}");
            return toothCollectables;
        }

        public static T AddAndSetValuesOfComponent<T>(this GameObject gameObject, T whatIsBeingCopiedFrom) where T : Component
        {
            T component = gameObject.AddComponent<T>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.CanWrite && propertyInfo.GetValue(whatIsBeingCopiedFrom) != null)
                {
                    propertyInfo.SetValue(component, propertyInfo.GetValue(whatIsBeingCopiedFrom));
                }
            }
            return component;
        }
    }
}