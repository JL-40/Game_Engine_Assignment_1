using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _Instance; // Singleton.

    [SerializeField] Canvas _Canvas;

    [SerializeField] PieceColor _CurrentPlayerTurn = PieceColor.White; // Tracks which player's turn it is.

    [SerializeField] GameObject _GameBoard; // Get the parent game object that holds the tiles that make up the game board.
    [SerializeField] List<Coordinate> _Tiles = new List<Coordinate>(); // 2D list of tiles that can be seen in the inspector.

    const int _BoardSize = 8;

    [SerializeField] List<GameObject> _CurrentWhitePieces = new List<GameObject>();
    [SerializeField] List<GameObject> _CurrentBlackPieces = new List<GameObject>();
    private void Awake()
    {
        // Write the singleton behaviour
        if (_Instance != null && _Instance != this)
        {
            Destroy(this); // Delete the duplicate GameManager
        }
        _Instance = this;

        if (_Canvas == null)
        {
            _Canvas = FindObjectOfType<Canvas>();
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
}

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


