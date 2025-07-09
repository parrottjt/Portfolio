using ChessAndCheckers.Board;
using System.Collections.Generic;
using System.Linq;

namespace ChessAndCheckers.Pieces.Chess
{
    public class Queen : Piece
    {
        public Queen(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, playerPiece, black)
        {
        }

        protected override string ImageResourceName => "Queen";

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
