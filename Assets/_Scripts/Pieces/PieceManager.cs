using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject piecePrefab;

    private List<Piece> whitePieces = null;
    private List<Piece> blackPieces = null;

    private enum p { Pawn, Rook, Knight, Bishop, King, Queen };

    private int[] pieceOrder = new int[16]
    {
        (int)p.Pawn, (int)p.Pawn, (int)p.Pawn, (int)p.Pawn, (int)p.Pawn, (int)p.Pawn, (int)p.Pawn, (int)p.Pawn,
        (int)p.Rook, (int)p.Knight, (int)p.Bishop, (int)p.King, (int)p.Queen, (int)p.Bishop, (int)p.Knight, (int)p.Rook
    };

    private Dictionary<int, Type> pieceLibrary = new Dictionary<int, Type>()
    {
        {(int)p.Pawn, typeof(Pawn)},
        {(int)p.Rook, typeof(Rook)},
        {(int)p.Knight, typeof(Knight)},
        {(int)p.Bishop, typeof(Bishop)},
        {(int)p.King, typeof(King)},
        {(int)p.Queen, typeof(Queen)},
    };

    public void Init(Board board)
    {
        whitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255), board);
        blackPieces = CreatePieces(Color.white, new Color32(210, 95, 64, 255), board);

        PlacePieces(1, 0, whitePieces, board);
        PlacePieces(6, 7, blackPieces, board);
    }

    private List<Piece> CreatePieces(Color teamColor, Color32 spriteColor, Board board)
    {
        List<Piece> newPieces = new List<Piece>();

        for (int i = 0; i < pieceOrder.Length; i++)
        {
            GameObject newPieceObject = Instantiate(piecePrefab);
            newPieceObject.transform.SetParent(transform);

            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;

            int key = pieceOrder[i];
            Type pieceType = pieceLibrary[key];

            Piece newPiece = (Piece)newPieceObject.AddComponent(pieceType);
            newPieces.Add(newPiece);

            newPiece.Setup(teamColor, spriteColor, this);
        }

        return newPieces;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<Piece> pieces, Board board)
    {
        for (int i = 0; i < 8; i++)
        {
            pieces[i].SetSpace(board.allSpaces[i, pawnRow]);

            pieces[i + 8].SetSpace(board.allSpaces[i, royaltyRow]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}