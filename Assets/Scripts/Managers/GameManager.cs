using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_CurrentPlayerTurn == PieceColor.Black)
        {

        }
        else if (_CurrentPlayerTurn == PieceColor.White)
        {

        }
    }

    // This function will fill the 2D list with each tile of the game board. This can then be used for moving the pieces.
    void GetBoardSquares()
    {
        for (int letter = 0; letter < _BoardSize; letter++)
        {
            List<GameObject> coordinates = new List<GameObject>();
            for (int number = 0; number < _BoardSize; number++)
            {
                coordinates.Add(_GameBoard.transform.GetChild(number).gameObject);
            }
            _GameBoardSquares.Add(coordinates);
        }
    }

    // Getter for the tiles that the pieces can access
    public List<List<GameObject>> BoardSquares {  get { return _GameBoardSquares; } }
}
