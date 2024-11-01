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
    [SerializeField] List<List<GameObject>> _GameBoardSquares = new List<List<GameObject>>(); // A 2D list that hold all tiles making a code version of the game board.

    [SerializeField] List<Dictionary<GameObject, GameObject>> _TestTileHolder = new List<Dictionary<GameObject, GameObject>>();

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
    }

    // Start is called before the first frame update
    void Start()
    {
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
        for (int letter = 0; letter < _BoardSize; letter++)
        {
            List<GameObject> coordinates = new List<GameObject>();
            Dictionary<GameObject, GameObject> Tile = new Dictionary<GameObject, GameObject>();
            for (int number = 0; number < _BoardSize; number++)
            {
                coordinates.Add(_GameBoard.transform.GetChild(number).gameObject);
                Tile.Add(_GameBoard.transform.GetChild(number).gameObject, null);
            }
            _GameBoardSquares.Add(coordinates);
            _TestTileHolder.Add(Tile);
        }
    }

    public void WhitePlayersTurn()
    {
        _CurrentPlayerTurn = PieceColor.White;

        foreach (GameObject whitePiece in _CurrentWhitePieces)
        {
            whitePiece.GetComponent<BaseChessPiece>().canMove = true;
        }

        foreach (GameObject blackPiece in _CurrentBlackPieces)
        {
            blackPiece.GetComponent<BaseChessPiece>().canMove = false;
        }
    }

    public void BlackPlayersTurn()
    {
        _CurrentPlayerTurn = PieceColor.Black;

        foreach (GameObject blackPiece in _CurrentBlackPieces)
        {
            blackPiece.GetComponent<BaseChessPiece>().canMove = true;
        }

        foreach (GameObject whitePiece in _CurrentWhitePieces)
        {
            whitePiece.GetComponent<BaseChessPiece>().canMove = false;
        }
    }

    /// <summary>
    /// Finds the tile in the GameBoardSquare 2D list.
    /// </summary>
    /// <param name="searchTile">The tile to find.</param>
    /// <returns>Returns the tile object if it is found. Returns null if the object is not in the list.</returns>
    public GameObject FindTile(GameObject searchTile)
    {
        foreach (var coordinateList in _GameBoardSquares)
        {
            foreach (var coordinate in coordinateList)
            {
                if (searchTile.name == coordinate.name)
                {
                    return coordinate;
                }
            }
        }
        return null;
    }

    public void OccupyTile(GameObject piece, bool unOccupy =  false)
    {
        foreach (Dictionary<GameObject, GameObject> coord in _TestTileHolder)
        {
            if (unOccupy)
            {
                coord.TryGetValue(piece, out GameObject tile);

                if (tile != null)
                {
                    coord[tile] = null;
                }
                
            }
            else
            {
                coord[piece.GetComponent<BaseChessPiece>().currentTile] = piece;
            }
        }
    }

    // Getter for the tiles that the pieces can access
    public List<List<GameObject>> BoardSquares {  get { return _GameBoardSquares; } }
}
