using System.Collections.Generic;
using ChessAndCheckers.Enums;
using ChessAndCheckers.Pieces;

namespace ChessAndCheckers.Board
{
    public static class BoardUtilities
    {
        public static bool ValidSpecificSquare(GameBoard board, BoardSquare boardSquare, int rowPosition, int columnPosition, out BoardSquare newSquare)
        {
            var newRow = boardSquare.X + rowPosition;
            var newColumn = boardSquare.Y + columnPosition;
            return board.SquareValid(newRow, newColumn, out newSquare);
        }

        public static bool NextValidSquareInDirection(GameBoard board, BoardSquare boardSquare, Direction direction, out BoardSquare newSquare)
        {
            var directionValue = Direction(direction);
            (int row, int col) currentSquare = (boardSquare.X, boardSquare.Y);
            board.SquareValid(currentSquare.row + directionValue.row, currentSquare.col + directionValue.column, out newSquare);
            return newSquare is not null;
        }

        public static List<BoardSquare> SquaresInDirection(GameBoard board, Piece piece, BoardSquare boardSquare, Direction direction)
        {
            var list = new List<BoardSquare>();
            AddToListInDirection(board, piece, boardSquare, list, Direction(direction));
            return list;
        }

        static void AddToListInDirection(GameBoard board, Piece piece, BoardSquare boardSquare, List<BoardSquare> list, (int row, int column) direction)
        {
            (int row, int col) currentSquare = (boardSquare.X, boardSquare.Y);
            if (board.SquareValid(currentSquare.row + direction.row, currentSquare.col + direction.column, out var newSquare))
            {
                list.Add(newSquare);
                if (piece.CanCapture(newSquare.Piece) == false)
                {
                    if (newSquare.Piece == null)
                    {
                        AddToListInDirection(board, piece, newSquare, list, direction);
                    }
                    else list.Remove(newSquare);
                }
            }
        }

        static (int row, int column) Direction(Direction direction)
        {
            return direction switch
            {
                Enums.Direction.Up => (1, 0),
                Enums.Direction.Down => (-1, 0),
                Enums.Direction.Left => (0, -1),
                Enums.Direction.Right => (0, 1),
                Enums.Direction.UpLeft => (1, -1),
                Enums.Direction.DownLeft => (-1, -1),
                Enums.Direction.UpRight => (1, 1),
                Enums.Direction.DownRight => (-1, 1)
            };
        }
    }
}
