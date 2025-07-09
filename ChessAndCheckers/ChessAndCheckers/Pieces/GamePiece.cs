using ChessAndCheckers.Board;

namespace ChessAndCheckers.Pieces
{
    public abstract class GamePiece
    {
        bool _captured;
        protected BoardSquare BoardSquare;
        public string Name { get; protected set; }
        public string Image { get; }
        public bool Captured => _captured;
        public bool PlayerPiece { get; }
        public bool IsBlack { get; }
        protected GamePiece(BoardSquare boardSquare, string name, string image, bool playerPiece, bool black)
        {
            BoardSquare = boardSquare;
            Name = name;
            Image = image;
            PlayerPiece = playerPiece;
            IsBlack = black;
        }

        public void Move(BoardSquare boardSquare)
        {
            UpdateBoardSquare(boardSquare);
        }

        public bool CanCapture(GamePiece gamePiece)
        {
            var val = false;
            if (gamePiece != null) val = PlayerPiece != gamePiece.PlayerPiece;

            return val;
        }

        public void Capture() => _captured = true;

        protected void UpdateBoardSquare(BoardSquare boardSquare) => BoardSquare = boardSquare;

        public abstract List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard);
    }
}
