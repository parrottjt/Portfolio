using TSCore.Utils;
using UnityEngine;

namespace TSCore.Pathfinding.Hex
{
    public static class HexMath
    {
        public static Vector3 CellPosition(int x, int z)
        {
            Vector3 position;
            position.x = (x + z * .5f - z /2) * (HexMetrics.INNER_RADIUS * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.OUTER_RADIUS * 1.5f);
            return position;
        }
    }
}
