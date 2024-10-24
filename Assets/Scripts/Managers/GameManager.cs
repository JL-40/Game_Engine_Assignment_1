using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField] PieceColor _CurrentPlayerTurn = PieceColor.Black;

    [SerializeField] GameObject _GameBoard;
    [SerializeField] List<List<GameObject>> _GameBoardSquares = new List<List<GameObject>>();

    const int _BoardSize = 8;

    // Start is called before the first frame update
    void Start()
    {
        if (_Instance != null)
        {
            Destroy(this);
        }
        _Instance = this;

        if (_GameBoard == null)
        {
            _GameBoard = GameObject.Find("GameBoard");
        }

        GetBoardSquares();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
