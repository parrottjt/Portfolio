using ChessAndCheckers.Board;
using ChessAndCheckers.Enums;
using System.Windows.Controls;

namespace ChessAndCheckers.Pieces.Chess
{
    public class King : GamePiece
    {
        public King(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, name, image, playerPiece, black)
        {
        }

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = new List<BoardSquare>();

            foreach (var direction in Enum.GetValues<Direction>())
            {
                Move(gameBoard, list, direction);
            }

            return list;
        }

        private void Move(GameBoard gameBoard, List<BoardSquare> list, Direction direction)
        {
            if (BoardUtilities.NextValidSquareInDirection(gameBoard, BoardSquare, direction, out var newSquare))
            {
                list.Add(newSquare);
                if (CanCapture(newSquare.GamePiece) == false && newSquare.GamePiece != null) list.Remove(newSquare); 
            }
        }
    }
}
