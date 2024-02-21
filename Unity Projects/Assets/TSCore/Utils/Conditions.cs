using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TSCore.Utils
{
    public static class Conditions
    {
        static bool _wallCheckInitialized;
        static int _wallCheck;

        public static int WallCheck<T>()
        {
            if (!_wallCheckInitialized)
            {
                _wallCheck = CreatWallLayerMask<T>();
            }

            return _wallCheck;
        }

        static LayerMask CreatWallLayerMask<T>()
        {
            _wallCheckInitialized = true;
            return LayerMask.GetMask(Enums.GetValuesAsStrings<T>());
        }

        public static bool ValueWithinRange(float value, (float min, float max) range)
        {
            return range.min <= value && value <= range.max;
        }

        //This determines if the distance between the transforms are less than or equal to a determined value.
        public static bool DistanceBetweenLessThan(Transform posA, Transform posB, float allowance) =>
            DistanceBetweenLessThan(posA.position, posB.position, allowance);

        //This determines if the distance between the vectors are less than or equal to a determined value.
        public static bool DistanceBetweenLessThan(Vector2 posA, Vector2 posB, float allowance) =>
            Vector2.Distance(posA, posB) <= allowance;

        //This determines if the distance between the transforms are greater than or equal to a determined value.
        public static bool DistanceBetweenGreaterThan(Transform posA, Transform posB, float allowance) =>
            Vector2.Distance(posA.position, posB.position) > allowance;

        //This determines if the distance between the vectors are greater than or equal to a determined value.
        public static bool DistanceBetweenGreaterThan(Vector2 posA, Vector2 posB, float allowance) =>
            Vector2.Distance(posA, posB) > allowance;

        public static bool ObjectXPositionGreaterThanOtherObjectXPosition(Transform objectAPosition,
            Transform objectBPosition) => objectAPosition.position.x >= objectBPosition.position.x;

        public static bool NotInWall<T>(Vector2 origin, Vector2 position) =>
            Physics2D.Linecast(origin, position, WallCheck<T>()) == false;

        public static bool AllConditionsMatchValue(bool value, params bool[] collection) =>
            collection.All(x => x == value);
        public static bool AllConditionsMatchValue(IEnumerable<bool> collection, bool value = true) =>
            collection.All(x => x == value);
    }
}