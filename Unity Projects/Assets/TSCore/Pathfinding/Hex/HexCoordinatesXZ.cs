using System;
using TSCore.Utils;
using UnityEngine;

namespace TSCore.Pathfinding.Hex
{
    [Serializable]
    public struct HexCoordinatesXZ
    {
        public int X { get; private set; }
        public int Y => -X - Z;
        public int Z { get; private set; }
        
        public HexCoordinatesXZ(int x, int z)
        {
            X = x;
            Z = z;
        }

        public static HexCoordinatesXZ FromOffsetCoordinates(int x, int z) =>
            new HexCoordinatesXZ(x - z / 2, z);

        public static HexCoordinatesXZ FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.INNER_RADIUS * 2f);
            float y = -x;
            float offset = position.z / (HexMetrics.OUTER_RADIUS * 3f);
            x -= offset;
            y -= offset;
            
            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x -y);
            
            if (iX + iY + iZ != 0) {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x -y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                }
                else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }
            if (iX + iY + iZ != 0)
            {
                DebugScript.LogWarning(typeof(HexCoordinatesXZ), "Rounding Error");
            }
            return new HexCoordinatesXZ(iX, iZ);
        }
        
        public override string ToString() => $"{{{X}, {Y}, {Z}}}";
        public string ToStringOnSeparateLines() => X + "\n" + Y + "\n" + Z;

        
    }

}