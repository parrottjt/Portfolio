using ChessAndCheckers.Board;
using ChessAndCheckers.GamePieces.Chess;
using ChessAndCheckers.Pages;
using ChessAndCheckers.Pieces;
using ChessAndCheckers.Pieces.Chess;

namespace ChessAndCheckers.Game
{
    public class ChessGame
    {
        public List<GamePiece> BlackPieces = new List<GamePiece>();
        public List<GamePiece> WhitePieces = new List<GamePiece>();

        ChessBoard _chessBoard;
        GameBoard _gameBoard;

        public bool IsPlayerWhite { get; private set; }

        public ChessGame(ChessBoard chessBoard, bool isPlayerWhite)
        {
            _chessBoard = chessBoard;
            _gameBoard = new GameBoard();
            IsPlayerWhite = isPlayerWhite;

            InitializeBlackPieces();
            InitializeWhitePieces();
        }

        void InitializeWhitePieces()
        {
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.A), "WhitePawn1", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.B), "WhitePawn2", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.C), "WhitePawn3", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.D), "WhitePawn4", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.E), "WhitePawn5", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.F), "WhitePawn6", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.G), "WhitePawn7", "/Images/Chess/Light Pawn", IsPlayerWhite, false));
            WhitePieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Two, Enums.Column.H), "WhitePawn8", "/Images/Chess/Light Pawn", IsPlayerWhite, false));

            WhitePieces.Add(new Rook(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.A), "WhiteRook1", "/Images/Chess/Light Rook", IsPlayerWhite, false));
            WhitePieces.Add(new Rook(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.H), "WhiteRook2", "/Images/Chess/Light Rook", IsPlayerWhite, false));

            WhitePieces.Add(new Knight(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.B), "WhiteKnight1", "/Images/Chess/Light Knight", IsPlayerWhite, false));
            WhitePieces.Add(new Knight(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.G), "WhiteKnight2", "/Images/Chess/Light Knight", IsPlayerWhite, false));

            WhitePieces.Add(new Bishop(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.C), "WhiteBishop1", "/Images/Chess/Light Bishop", IsPlayerWhite, false));
            WhitePieces.Add(new Bishop(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.F), "WhiteBishop2", "/Images/Chess/Light Bishop", IsPlayerWhite, false));

            WhitePieces.Add(new Queen(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.D), "WhiteQueen", "/Images/Chess/Light Queen", IsPlayerWhite, false));
            WhitePieces.Add(new King(_gameBoard.GetBoardSquare(Enums.Row.One, Enums.Column.E), "WhiteKing", "/Images/Chess/Light King", IsPlayerWhite, false));
        }

        void InitializeBlackPieces()
        {
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.A), "BlackPawn1", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.B), "BlackPawn2", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.C), "BlackPawn3", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.D), "BlackPawn4", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.E), "BlackPawn5", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.F), "BlackPawn6", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.G), "BlackPawn7", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));
            BlackPieces.Add(new Pawn(_gameBoard.GetBoardSquare(Enums.Row.Seven, Enums.Column.H), "BlackPawn8", "/Images/Chess/Dark Pawn", IsPlayerWhite == false, true));

            BlackPieces.Add(new Rook(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.A), "BlackRook1", "/Images/Chess/Dark Rook", IsPlayerWhite == false, true));
            BlackPieces.Add(new Rook(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.H), "BlackRook2", "/Images/Chess/Dark Rook", IsPlayerWhite == false, true));

            BlackPieces.Add(new Knight(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.B), "BlackKnight1", "/Images/Chess/Dark Knight", IsPlayerWhite == false, true));
            BlackPieces.Add(new Knight(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.G), "BlackKnight2", "/Images/Chess/Dark Knight", IsPlayerWhite == false, true));

            BlackPieces.Add(new Bishop(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.C), "BlackBishop1", "/Images/Chess/Dark Bishop", IsPlayerWhite == false, true));
            BlackPieces.Add(new Bishop(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.F), "BlackBishop2", "/Images/Chess/Dark Bishop", IsPlayerWhite == false, true));

            BlackPieces.Add(new Queen(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.D), "BlackQueen", "/Images/Chess/Dark Queen", IsPlayerWhite == false, true));
            BlackPieces.Add(new King(_gameBoard.GetBoardSquare(Enums.Row.Eight, Enums.Column.E), "BlackKing", "/Images/Chess/Dark King", IsPlayerWhite == false, true));
        }
    }
}
