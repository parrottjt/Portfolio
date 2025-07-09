using System.Collections.Generic;
using ChessAndCheckers.Board;
using ChessAndCheckers.Enums;
using ChessAndCheckers.Pieces;

namespace ChessAndCheckers.GamePieces.Chess
{
    public class Pawn : Piece
    {
        public Pawn(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, playerPiece, black)
        {
        }

        protected override string ImageResourceName => "Pawn";

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = new List<BoardSquare>();
            

            if (IsBlack == true)
            {
                VerticalMoveCheck(gameBoard, list, -1, 0);
                VerticalMoveCheck(gameBoard, list, -2, 0);
                DiagonalCaptureCheck(gameBoard, list, Direction.DownLeft);
                DiagonalCaptureCheck(gameBoard, list, Direction.DownRight);
            }
            else
            {
                VerticalMoveCheck(gameBoard, list, 1, 0);
                VerticalMoveCheck(gameBoard, list, 2, 0);
                DiagonalCaptureCheck(gameBoard, list, Direction.UpLeft);
                DiagonalCaptureCheck(gameBoard, list, Direction.UpRight);
            }
            return list;
        }

        void VerticalMoveCheck(GameBoard gameBoard, List<BoardSquare> list, int rowAdustment, int columnAdjustment)
        {
            if (BoardUtilities.ValidSpecificSquare(gameBoard, BoardSquare, rowAdustment, columnAdjustment, out var newSquare))
            {
                if (newSquare.Piece is null) list.Add(newSquare);
            }
        }

        private void DiagonalCaptureCheck(GameBoard gameBoard, List<BoardSquare> list, Direction direction)
        {
            if(BoardUtilities.NextValidSquareInDirection(gameBoard, BoardSquare, direction, out var newSquare))
            {
                if(CanCapture(newSquare.Piece)) list.Add(newSquare);
            }
        }
    }
}
