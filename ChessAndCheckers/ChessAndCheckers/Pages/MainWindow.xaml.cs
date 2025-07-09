using ChessAndCheckers.Game;
using ChessAndCheckers.Pages;
using ChessAndCheckers.Pieces;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessAndCheckers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChessBoard _chessBoard;

        ChessGame _chessGame;

        bool ChoseGame { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            SetupGame();

        }

        void SetupGame()
        {
            _chessBoard = new ChessBoard();
            _chessGame = new ChessGame(_chessBoard, true);
            Main.Content = _chessBoard;

            //_chessBoard.WhitePawn1.Source = PieceImage(_chessBoard.WhitePawn1.Name);
        }

        BitmapImage PieceImage(string pieceName)
        {
            BitmapImage bitmapImage = new BitmapImage();

            GamePiece piece;
            if (pieceName.Contains("White"))
            {
                piece = _chessGame.WhitePieces.First(piece => piece.Name == pieceName);
            }
            else piece = _chessGame.BlackPieces.First(piece => piece.Name == pieceName);

            bitmapImage.UriSource = new Uri(piece.Image, UriKind.Relative);
            return bitmapImage;
        }

    }
}