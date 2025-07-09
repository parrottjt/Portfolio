using ChessAndCheckers.Board;

using System.Collections.Generic;


namespace ChessAndCheckers.Pieces.Chess
{
    public class Knight : Piece
    {
        public Knight(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, playerPiece, black)
        {
        }

        protected override string ImageResourceName => "Knight";

        public override List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard)
        {
            var list = new List<BoardSquare>();
            KnightSquareCheck(gameBoard, list, 2, 1);
            KnightSquareCheck(gameBoard, list, 2, -1);
            KnightSquareCheck(gameBoard, list, 1, 2);
            KnightSquareCheck(gameBoard, list, 1, -2);
            KnightSquareCheck(gameBoard, list, -2, 1);
            KnightSquareCheck(gameBoard, list, -2, -1);
            KnightSquareCheck(gameBoard, list, -1, 2);
            KnightSquareCheck(gameBoard, list, -1, -2);

            return list;
        }

        void KnightSquareCheck(GameBoard gameBoard, List<BoardSquare> list, int rowAdjustment, int columnAdjustment)
        {
            
            if (gameBoard.SquareValid(BoardSquare.X + rowAdjustment, BoardSquare.Y + columnAdjustment, out var square))
            {
                list.Add(square);
                if (CanCapture(square.Piece) == false)
                {
                    list.Remove(square);
                }
            }
        }
    }
}
