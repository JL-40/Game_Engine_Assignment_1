using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [Range(1,2)]
    [SerializeField] int _currentPlayerTurn = 1;

    [SerializeField] GameObject _GameBoard;
    [SerializeField] List<List<GameObject>> _GameBoardSquares = new List<List<GameObject>>();

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
