using System.Collections;
using System.Collections.Generic;
using ChessAndCheckers.Pieces;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    enum ChessPiece
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    [SerializeField] ChessPiece _chessPiece;
    Piece _piece;
    
    
    
}
