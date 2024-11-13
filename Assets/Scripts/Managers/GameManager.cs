using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class GameManager : MonoBehaviour, Observer
{
    public static GameManager _Instance; // Singleton.

    [SerializeField] Canvas _Canvas;
    [SerializeField] public TMP_Text _PromotionText;

    [SerializeField] PieceColor _CurrentPlayerTurn = PieceColor.White; // Tracks which player's turn it is.

    [SerializeField] GameObject _GameBoard; // Get the parent game object that holds the tiles that make up the game board.
    [SerializeField] List<Coordinate> _Tiles = new List<Coordinate>(); // 2D list of tiles that can be seen in the inspector.

    const int _BoardSize = 8;

    [SerializeField] List<GameObject> _CurrentWhitePieces = new List<GameObject>();
    [SerializeField] List<GameObject> _CurrentBlackPieces = new List<GameObject>();


    [Header("Command")]
    [Space(10)]
    [SerializeField] Invoker _Invoker;

    [Header("Observer")]
    [Space(10)]
    [SerializeField] List<Pawn> _Pawns = new List<Pawn>();
    [SerializeField] List<GameObject> _WhitePromotionList = new List<GameObject>();
    [SerializeField] List<GameObject> _BlackPromotionList = new List<GameObject>();

    private void Awake()
    {
        // Write the singleton behaviour
        if (_Instance != null && _Instance != this)
        {
            Destroy(this); // Delete the duplicate GameManager
        }
        _Instance = this;

        if (_Invoker == null)
        {
            _Invoker = GetComponent<Invoker>();
        }

        if (_Canvas == null)
        {
            _Canvas = FindObjectOfType<Canvas>();
        }

        if (_PromotionText == null)
        {
            _PromotionText = GameObject.Find("PromotionText").GetComponent<TMP_Text>();
        }

        // Finds the game board object if I didn't add it though the inspector (prevents errors).
        if (_GameBoard == null)
        {
            _GameBoard = GameObject.Find("GameBoard");
        }

        GetBoardSquares();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Clear out the list of pieces on the board.
        _CurrentBlackPieces.Clear();
        _CurrentWhitePieces.Clear();

        _CurrentWhitePieces = GameObject.FindGameObjectsWithTag("White").ToList();
        _CurrentBlackPieces = GameObject.FindGameObjectsWithTag("Black").ToList();

        WhitePlayersTurn();

        _PromotionText.gameObject.SetActive(false);

        GetAllPawns();
    }

    // Update is called once per frame
    void Update()
    { 

    }

    // This function will fill the 2D list with each tile of the game board. This can then be used for moving the pieces.
    void GetBoardSquares()
    {
        int count = 0;
        List<GameObject> temp = new List<GameObject>();
        foreach (Transform tile in _GameBoard.transform)
        {
            count++;
            temp.Add(tile.gameObject);

            if (count == _BoardSize)
            {
                _Tiles.Add(new Coordinate(temp));
                count = 0;
                temp.Clear();
            }
        }
    }

    /// <summary>
    /// Swaps the current player's turn to white. Disabling all black pieces ability to move and enabling all white pieces to move.
    /// </summary>
    public void WhitePlayersTurn()
    {
        _CurrentPlayerTurn = PieceColor.White;

        foreach (GameObject whitePiece in _CurrentWhitePieces)
        {
            whitePiece.GetComponent<BaseChessPiece>().PlayersTurn();
        }

        foreach (GameObject blackPiece in _CurrentBlackPieces)
        {
            blackPiece.GetComponent<BaseChessPiece>().PlayersTurn(false);
        }
    }
    /// <summary>
    /// Swaps the current player's turn to black. Disabling all white pieces ability to move and enabling all black pieces to move.
    /// </summary>
    public void BlackPlayersTurn()
    {
        _CurrentPlayerTurn = PieceColor.Black;

        foreach (GameObject blackPiece in _CurrentBlackPieces)
        {
            blackPiece.GetComponent<BaseChessPiece>().PlayersTurn();
        }

        foreach (GameObject whitePiece in _CurrentWhitePieces)
        {
            whitePiece.GetComponent<BaseChessPiece>().PlayersTurn(false);
        }
    }

    /// <summary>
    /// Finds the tile in the List.
    /// </summary>
    /// <param name="searchTile">The tile to find.</param>
    /// <returns>Returns the tile object if it is found. Returns null if the object is not in the list.</returns>
    public GameObject FindTile(GameObject searchTile)
    {
        foreach (Coordinate coord in _Tiles)
        {
            foreach (GameObject tile in coord._Tiles)
            {
                if (tile == searchTile)
                {
                    return tile;
                }
            }
        }
        return null;
    }
    public List<Coordinate> GetTiles { get { return _Tiles; } }

    public int GetMaxBoardSize { get { return _BoardSize; } }
    

    // COMMAND
    public Invoker CommandInvoker { get { return _Invoker; } }

    // OBSERVER
    void OnDestroy()
    {
        if (_Pawns.Count != 0)
        {
            foreach (Pawn pawn in _Pawns)
            {
                pawn.UnsubscribeToSubject(this);
            }
        }
    }

    public void Notify(Subject subject)
    {
        if (subject is Pawn pawn)
        {
            PromotePawn(pawn);
        }
    }

    public void NotifyDirtyFlag(Subject subject)
    {
        if (subject is Pawn pawn && pawn.IsDirty)
        {
            PromotePawn(pawn);
        }
    }

    /// <summary>
    /// Gets all pawn pieces to subscribe too
    /// </summary>
    void GetAllPawns()
    {
        foreach (Pawn pawn in GameObject.FindObjectsOfType<Pawn>())
        {
            pawn.SubscribeToSubject(this);
            _Pawns.Add(pawn);
        }
    }

    /// <summary>
    /// Function that promotes the pawn into other pieces excluding the King
    /// </summary>
    public void PromotePawn(Pawn pawn)
    {
        GameManager._Instance._PromotionText.gameObject.SetActive(true);

        GameObject promotedPawn = null;

        // Input to promote pawn
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Promote to Rook
        {
            promotedPawn = Instantiate(pawn.ChessColor == PieceColor.White ? _WhitePromotionList[0] : _BlackPromotionList[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Promote to Knight
        {
            promotedPawn = Instantiate(pawn.ChessColor == PieceColor.White ? _WhitePromotionList[1] : _BlackPromotionList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Promote to Bishop
        {
            promotedPawn = Instantiate(pawn.ChessColor == PieceColor.White ? _WhitePromotionList[2] : _BlackPromotionList[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //Promote to Queen
        {
            promotedPawn = Instantiate(pawn.ChessColor == PieceColor.White ? _WhitePromotionList[3] : _BlackPromotionList[3]);
        }

        // Error, no promoted pawn
        if (promotedPawn == null)
        {
            return;
        }

        // Set up promoted pawn.
        promotedPawn.name = $"{pawn.gameObject.name} (Promoted)";

        BaseChessPiece promotedPawnBase = promotedPawn.GetComponent<BaseChessPiece>();
        promotedPawnBase.currentTile = pawn.currentTile;

        promotedPawnBase.PieceMovement.SetTilePosition(pawn.currentTile);

        Destroy(pawn.gameObject); // Destroy pawn

        _PromotionText.gameObject.SetActive(false);
    }

}


// FOR SERIALIZABLE 2D LIST
/// <summary>
/// This is just to make a 2D list that can be seen in the inspector for debugging.
/// </summary>
[System.Serializable]
public struct Coordinate
{
    [SerializeField] public List<GameObject> _Tiles;

    public Coordinate(List<GameObject> tiles)
    {
        _Tiles = new List<GameObject>();
        foreach (GameObject tile in tiles)
        {
            _Tiles.Add(tile);
        }
    }
}


