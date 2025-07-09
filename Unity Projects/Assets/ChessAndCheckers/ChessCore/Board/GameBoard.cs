using ChessAndCheckers.Enums;
using TSCore.Pathfinding;
using UnityEngine;

namespace ChessAndCheckers.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] float _cellSize;

        GridXY<BoardSquare> _grid;


        void Awake()
        {
            _grid = new GridXY<BoardSquare>(8, 8, _cellSize, transform,
                (xy, i, arg3) => new BoardSquare(xy, i, arg3), false);
        }

        public bool SquareValid(int row, int column, out BoardSquare boardSqaure)
        {
            boardSqaure = null;
            var val = _grid.CellInGrid((row, column));
            if (val)
            {
                boardSqaure = _grid.GetGridObject(row, column);
            }

            return val;
        }

        public BoardSquare GetBoardSquare(Row row, Column column) => GetBoardSquare((int)row, (int)column);
        public BoardSquare GetBoardSquare(int x, int y) => _grid.GetGridObject(x, y);
    }
}