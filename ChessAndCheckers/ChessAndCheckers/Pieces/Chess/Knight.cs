using ChessAndCheckers.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessAndCheckers.Pieces.Chess
{
    public class Knight : GamePiece
    {
        public Knight(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black) : base(boardSquare, name, image, playerPiece, black)
        {
        }

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
            
            if (gameBoard.SquareValid(BoardSquare.Square.row + rowAdjustment, BoardSquare.Square.column + columnAdjustment, out var square))
            {
                list.Add(square);
                if (CanCapture(square.GamePiece) == false)
                {
                    list.Remove(square);
                }
            }
        }
    }
}
