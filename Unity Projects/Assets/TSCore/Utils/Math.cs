using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TSCore.Utils
{
    public static class Math
    {
        public static int Factorial(int number)
        {
            if (number == 1)
            {
                return 1;
            }
            else if(number == 0)
            {
                return 1;
            }
            return number * Factorial(number - 1);
        }
    
        public static Vector2 CreateRandomNormalizedVector()
        {
            return CreateRandomNormalizedVectorFromRange(-1f, 1f, -1f, 1f);
        }

        public static Vector2 CreateRandomNormalizedVectorFromRange(float xMin, float xMax, float yMin, float yMax)
        {
            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);
            return new Vector2(x, y).normalized;
        }

        public static Vector2 CreatePositionInsideRadius(Vector2 center, float radius)
        {
            while (true)
            {
                var x = RandomValueBasedOnBounds(radius, center.x);
                var y = RandomValueBasedOnBounds(radius, center.y);
                var position = new Vector2(x, y);
                if (Conditions.DistanceBetweenLessThan(position, center, radius)) return position;
            }
        }

        public static Ray2D GetRay(Vector2 origin, Vector2 destination)
        {
            var ray = new Ray2D(origin, (destination - origin).normalized);
            return ray;
        }
        
        public static Ray2D ReflectOnCollision(Vector2 origin, Vector2 collisionPoint, Vector2 collisionNormal)
        {
            var initialRay = GetRay(origin, collisionPoint);
            var reflectedDirection = Vector2.Reflect(initialRay.direction, collisionNormal).normalized;
            
            return new Ray2D(collisionPoint, reflectedDirection);
        }
        
        public static float RandomValueBasedOnBounds(float radius, float objectCenter) =>
            Random.Range(-radius + objectCenter, radius + objectCenter);
        
        public static Quaternion RotationLookAtXAxis(Transform startTransform, Transform endVector)
        {
            return Quaternion.AngleAxis(GetRotationAngle(startTransform.position, endVector.position), Vector3.forward);
        }
        public static Quaternion RotationLookAtXAxis(Vector2 startVector, Vector2 endVector)
        {
            return Quaternion.AngleAxis(GetRotationAngle(startVector, endVector), Vector3.forward);
        }

        public static Quaternion RotationLookAtYAxis(Transform startTransform, Transform endVector)
        {
            return Quaternion.AngleAxis(GetRotationAngle(startTransform.position, endVector.position) - 90f, Vector3.forward);
        }
        public static Quaternion RotationLookAtYAxis(Vector2 startVector, Vector2 endVector)
        {
            return Quaternion.AngleAxis(GetRotationAngle(startVector, endVector) - 90f, Vector3.forward);
        }
        
        public static float GetRotationAngle(Vector2 startVector, Vector2 endVector)
        {
            Vector2 directionVector = startVector - endVector;
            return Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        }

        public static T AddValues<T>(params T[] values)
        {
            T sum = default;
            if (typeof(T) == typeof(int))
            {
                int sumOfValues = 0;   

                foreach (int value in values as int[])
                {
                    sumOfValues += value;
                }

                sum = (T) Convert.ChangeType(sumOfValues, typeof(T));
            }

            return sum;
        }
    }
}
