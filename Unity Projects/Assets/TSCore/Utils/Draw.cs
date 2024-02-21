using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSCore.Utils
{
    public class Draw : MonoBehaviour
    {
        public static void LineBetweenToPoints(Vector3 startPosition, Vector3 endPosition)
        {
            
        }

        public static void VectorDirectionArrow(Vector3 startPosition, Vector3 direction, float distance = 5)
        {
            var endpoint = startPosition + (direction * distance);
            var arrowLeftDirection = Vector3.Cross(direction * distance, Vector3.left * distance);
            Debug.DrawRay(endpoint, arrowLeftDirection * (distance * .25f), Color.yellow);
            Debug.DrawRay(startPosition, direction * distance, Color.red);
        }
    }
}
