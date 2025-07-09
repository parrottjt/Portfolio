using ChessAndCheckers.Enums;
using ChessAndCheckers.Pieces;

namespace ChessAndCheckers.Board
{
    public class BoardSquare
    {
        public string Row { get; }
        public string Column { get; }
        public (int row, int column) Square;
        public GamePiece GamePiece { get; private set; }
        public BoardSquare(Row row, Column column, GamePiece gamePiece)
        {
            Row = row.ToString().ToLower();
            Column = ((int)column).ToString();
            Square = ((int)row - 1, (int)column - 1);
            GamePiece = gamePiece;
        }
        public void ChangeGamePiece(GamePiece gamePiece)
        {
            GamePiece = gamePiece;
        }
    }
}
