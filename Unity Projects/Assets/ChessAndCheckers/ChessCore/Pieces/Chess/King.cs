using System;
using System.Collections.Generic;
using ChessAndCheckers.Board;
using ChessAndCheckers.Enums;

namespace ChessAndCheckers.Pieces.Chess
{
    public class King : Piece
    {
        public King(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, playerPiece, black)
        {
        }

        protected override string ImageResourceName => "King";

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = new List<BoardSquare>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
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
                if (CanCapture(newSquare.Piece) == false && newSquare.Piece != null) list.Remove(newSquare); 
            }
        }
    }
}
