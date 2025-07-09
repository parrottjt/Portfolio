using System.Collections.Generic;
using ChessAndCheckers.Board;
using UnityEngine;
using UnityEngine.UI;

namespace ChessAndCheckers.Pieces
{
    public abstract class Piece 
    {
        bool _captured;
        protected abstract string ImageResourceName { get; }
        protected BoardSquare BoardSquare;
        public Image Image { get; }
        public bool Captured => _captured;
        public bool PlayerPiece { get; }
        public bool IsBlack { get; }
        protected Piece(BoardSquare boardSquare, bool playerPiece, bool black)
        {
            BoardSquare = boardSquare;
            PlayerPiece = playerPiece;
            IsBlack = black;
            Image = Resources.Load<Image>($"ChessAndCheckers/Chess/{(black ? "Dark" : "Light")} {ImageResourceName}");
        }

        public void Move(BoardSquare boardSquare)
        {
            UpdateBoardSquare(boardSquare);
        }

        public bool CanCapture(Piece piece)
        {
            var val = false;
            if (piece != null) val = PlayerPiece != piece.PlayerPiece;

            return val;
        }

        public void Capture() => _captured = true;

        protected void UpdateBoardSquare(BoardSquare boardSquare) => BoardSquare = boardSquare;

        public abstract List<BoardSquare> PossibleMoveSquares(GameBoard gameBoard);
    }
}
