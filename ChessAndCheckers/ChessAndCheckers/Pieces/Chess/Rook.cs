using ChessAndCheckers.Board;
using System.Windows.Controls;

namespace ChessAndCheckers.Pieces.Chess
{
    public class Rook : GamePiece
    {
        public Rook(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, name, image, playerPiece, black)
        {
        }

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Left);
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Right));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Up));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Down));

            return list;
        }
    }
}
