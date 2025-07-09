using ChessAndCheckers.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessAndCheckers.Pieces.Chess
{
    public class Queen : GamePiece
    {
        public Queen(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, name, image, playerPiece, black)
        {
        }

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Left);
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Right));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Up));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.Down));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.UpLeft));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.UpRight));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.DownLeft));
            list.Concat(BoardUtilities.SquaresInDirection(gameBoard, this, BoardSquare, Enums.Direction.DownRight));

            return list;
        }
    }
}
