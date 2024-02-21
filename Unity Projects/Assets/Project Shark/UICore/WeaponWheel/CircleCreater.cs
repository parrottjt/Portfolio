using UnityEngine;

namespace UICore.WeaponWheel
{
    public class CircleCreater : MonoBehaviour
    {
        public int numberOfCircles;
        [SerializeField] float radius;
        [SerializeField] GameObject projectileType, parent;

        int count;

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < numberOfCircles; i++)
            {
                if (count != numberOfCircles)
                {
                    float angle = i * Mathf.PI * 2 / numberOfCircles;
                    Vector3 newPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                    var clone = Instantiate(projectileType, newPos, Quaternion.Euler(0, 0, 0));
                    clone.transform.parent = parent.transform;
                    clone.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                    count++;
                }
            }
        }

        public void ResetCount()
        {
            count = 0;
        }
    }
}
