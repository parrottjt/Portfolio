using System.Collections.Generic;
using FrikinCore.Enumeration;
using FrikinCore.Utils;
using TSCore;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;
using Math = TSCore.Utils.Math;
using Random = UnityEngine.Random;

namespace FrikinCore.Loot.Teeth
{
    /// <summary>
    /// If we want 45-60 teeth to spawn
    /// find random number in range of min and max
    /// see if you can get 45-60 teeth
    /// if not pick another number
    ///
    /// 250 - 500
    /// 385 - target
    /// 38 - 10 value
    /// 1 - 5 value
    ///
    /// 342 - target
    /// 34 - 10 value
    /// 2 - 1 value
    /// 
    /// 77 teeth flat
    /// 492
    /// 19 - 25 value 16/4 = 4-12 = 8
    /// 492 - 200 = 292
    /// 29 - 10 value 
    /// </summary>
    public class TeethExplosion : MonoBehaviour
    {
        [Range(100, 2000)] public int minScoreRange, maxScoreRange;

        //Radial Teeth Explosion Zone Variable
        [SerializeField] float radius = 15;
        readonly List<List<Vector2>> teethPositions = new();
        public List<GameObject> teeth = new();

        List<GameObject> toothCollectables;

        int[] numberOfEachToothCollectable;
        int totalNumberOfTeeth;
        int valueOfTeethToSpawn;

        int firePositionIndexForTeethExplosion = 1;
        bool runExplosion;

        void RunCalculations(Vector2 center)
        {
            var time = Time.realtimeSinceStartup;
            valueOfTeethToSpawn = Random.Range(minScoreRange, maxScoreRange + 1);
            numberOfEachToothCollectable = Handlers.GetToothCollectablesFromScoreCalculation(valueOfTeethToSpawn,
                out toothCollectables, Handlers.DesiredScoreCalculationType.RandomizeCount);
            foreach (var numberOfToothCollectable in numberOfEachToothCollectable)
            {
                totalNumberOfTeeth += numberOfToothCollectable;
            }

            CreateTeethPositionList(center);
            TickTimeTimer.OnTick += OnTick;
            //print($"Time for calculations was :{Time.realtimeSinceStartup - time}");
        }

        void OnDestroy() => TickTimeTimer.OnTick -= OnTick;

        public void TestTeethExplosion()
        {
            RunCalculations(transform.position);
            runExplosion = true;
        }

        public void TriggerTeethExplosion(Vector2 spawnLocation)
        {
            RunCalculations(spawnLocation);
            runExplosion = true;
        }

        void CreateTeethPositionList(Vector2 center)
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < totalNumberOfTeeth; i++)
            {
                if (count >= numberOfEachToothCollectable[index])
                {
                    index++;
                    if (index >= toothCollectables.Count - 1)
                    {
                        index = toothCollectables.Count - 1;
                    }

                    count = 0;
                }

                count++;

                teethPositions.Add(CreateToothPositions(center));

                //Get choice between the two objects
                var tooth = ObjectPooling.GetAvailable(toothCollectables[index]);
                tooth.transform.position = transform.position;
                tooth.SetActive(true);
                tooth.GetComponent<ToothMovementForExplosion>().enabled = true;
                teeth.Add(tooth);
            }

            Handlers.SetActiveOnGameObjectsTo(false, teeth.ToArray());
        }

        List<Vector2> CreateToothPositions(Vector2 center)
        {
            List<Vector2> toothPositions = new List<Vector2>();

            var loopBreak = 0;
            while (loopBreak < 100)
            {
                loopBreak += 1;
                var destination = Math.CreatePositionInsideRadius(center, radius);
                CreateToothPosition(toothPositions, center, destination, radius);
                return toothPositions;
            }

            return toothPositions;
        }

        void CreateToothPosition(List<Vector2> toothPositions, Vector2 origin, Vector2 destination, float distance)
        {
            if (Conditions.NotInWall<GameEnums.WallLayers>(origin, destination) == false)
            {
                var startRay = Math.GetRay(origin, destination);
                var hit = Handlers.RaycastAgainstWall(startRay, distance);
                toothPositions.Add(hit.point);
                var updatedDistance = distance - hit.distance;
                var reflectedRay = Math.ReflectOnCollision(origin, hit.point, hit.normal);

                CreateToothPosition(toothPositions, reflectedRay.origin,
                    (reflectedRay.origin + (reflectedRay.direction.normalized * updatedDistance)),
                    updatedDistance);
            }
            else
            {
                toothPositions.Add(destination);
            }
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            if (runExplosion)
            {
                for (int i = firePositionIndexForTeethExplosion; i < firePositionIndexForTeethExplosion + 5; i++)
                {
                    if (i < totalNumberOfTeeth)
                    {
                        teeth[i].SetActive(true);
                        teeth[i].GetComponent<ToothMovementForExplosion>().SetMovePositions(teethPositions[i]);
                    }
                }

                if (firePositionIndexForTeethExplosion < totalNumberOfTeeth - 1)
                {
                    firePositionIndexForTeethExplosion += 5;
                }
                else
                {
                    runExplosion = false;
                }
            }
        }
    }
}