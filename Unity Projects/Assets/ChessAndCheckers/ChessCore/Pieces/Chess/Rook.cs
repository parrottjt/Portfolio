using System.Collections.Generic;
using System.Linq;
using ChessAndCheckers.Board;

namespace ChessAndCheckers.Pieces.Chess
{
    public class Rook : Piece
    {
        public Rook(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, playerPiece, black)
        {
        }

        protected override string ImageResourceName => "Rook";

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
