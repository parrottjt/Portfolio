using UnityEngine;

namespace TSCore.Pathfinding
{
    public class NodeXZ : MonoBehaviour
    {
        protected GridXZ<NodeXZ> _grid;
        public int X { get; set; }
        public int Y { get; set; }

        public NodeXZ(GridXZ<NodeXZ> grid, int x, int y)
        {
            _grid = grid;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
