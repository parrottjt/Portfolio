using ChessAndCheckers.Enums;
using ChessAndCheckers.Pieces;
using TSCore.Pathfinding;

namespace ChessAndCheckers.Board
{
    public class BoardSquare
    {
        protected GridXY<BoardSquare> _grid;
        public int X { get; set; }
        public int Y { get; set; }
        
        public Piece Piece { get; private set; }
        public BoardSquare(GridXY<BoardSquare> grid, int x, int y)
        {
            _grid = grid;
            X = x;
            Y = y;
        }
        public void ChangeGamePiece(Piece piece)
        {
            Piece = piece;
        }
        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
