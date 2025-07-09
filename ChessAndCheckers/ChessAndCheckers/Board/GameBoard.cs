using ChessAndCheckers.Enums;

namespace ChessAndCheckers.Board
{
    public class GameBoard
    {
        BoardSquare[,] _grid = new BoardSquare[8, 8];

        public int RowCount { get; }
        public int ColumnCount { get; }

        public GameBoard()
        {
            RowCount = _grid.GetLength(0);
            ColumnCount = _grid.GetLength(1);
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    _grid[row, col] = new BoardSquare((Row)row, (Column)col, null);
                }
            }
        }

        public bool SquareValid(int row, int column, out BoardSquare boardSqaure)
        {
            boardSqaure = null;

            if ((row >= 0 && row < RowCount) &&
                (column >= 0 && column < ColumnCount))
            {
                boardSqaure = GetBoardSquare(row, column);
                return true;
            }
            else return false;
        }
        public BoardSquare GetBoardSquare(Row row, Column column) => GetBoardSquare((int)row, (int)column);
        public BoardSquare GetBoardSquare(int row, int column) => _grid[row, column];
    }
}
