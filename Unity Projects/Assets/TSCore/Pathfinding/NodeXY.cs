
namespace TSCore.Pathfinding
{
    public class NodeXY
    {
        protected GridXY<NodeXY> _grid;
        public int X { get; set; }
        public int Y { get; set; }

        public NodeXY(GridXY<NodeXY> grid, int x, int y)
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
