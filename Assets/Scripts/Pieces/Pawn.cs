using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pawn : BaseChessPiece, Subject
{
    [SerializeField] List<Observer> observers = new List<Observer>();
    [SerializeField] bool isDirty = false;
    [SerializeField] bool useDirtyFlag = false;

    protected override void Awake()
    {
        base.Awake();

        pieceMovement = new PieceMovement(this.gameObject);
    }

    void Update()
    {
        CheckForPromotion();
    }

    public override List<GameObject> ValidMoves()
    {
        List<GameObject> validTiles = new List<GameObject>();

        char currLetter = currentTile.name[0];
        int currNumber = currentTile.name[1] - '0';

        List<string> validTileNames = new List<string>();

        // Ensures we don't try to get a row number beyond the size of the board. Max is 8 for standard chess.
        if (currNumber + 1 <= GameManager._Instance.GetMaxBoardSize)
        {
            validTileNames.Add($"{currLetter}{currNumber + 1}");
        }

        // For left-forward-diagonal tiles
        if (currLetter != 'A' && currNumber + 1 <= GameManager._Instance.GetMaxBoardSize)
        {
            validTileNames.Add($"{(char)(currLetter - 1)}{currNumber + 1}");
        }

        // For right-forward-diagonal tiles
        if (currLetter != 'H' && currNumber + 1 <= GameManager._Instance.GetMaxBoardSize)
        {
            validTileNames.Add($"{(char)(currLetter + 1)}{currNumber + 1}");
        }

        foreach (string searchName in validTileNames)
        {
            validTiles.Add(
                GameManager._Instance.FindTile(
                    GameObject.Find(searchName)
                    )
                );

        }

        return validTiles;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        GameManager._Instance.CommandInvoker.ExecuteCommand(PieceMovement);
    }

    public override void Capture(GameObject enemyPiece)
    {
        string enemyTileName = enemyPiece.GetComponent<BaseChessPiece>().currentTile.name;

        // Ensures that the tile must be diagonal to capture for pawns.
        if ((enemyTileName[0] == currentTile.name[0] - 1 || enemyTileName[0] == currentTile.name[0] + 1) && enemyTileName[1] == currentTile.name[1] + 1)
        {
            base.Capture(enemyPiece);
        }
    }

    // OBSERVER
    public void SubscribeToSubject(Observer observer)
    {
        observers.Add(observer);
    }

    public void UnsubscribeToSubject(Observer observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (Observer observer in observers)
        {
            observer.Notify(this);
        }
    }

    public void CheckForPromotion()
    {
        if (currentTile.name.Contains("1") || currentTile.name.Contains($"{GameManager._Instance.GetMaxBoardSize}"))
        {
            if (useDirtyFlag)
            {
                isDirty = true;
                NotifyObserversDirtyFlag();
            }

            NotifyObservers();
        }
    }

    // DIRTY FLAG
    public void NotifyObserversDirtyFlag()
    {
        if (isDirty)
        {
            foreach (Observer observer in observers)
            {
                observer.NotifyDirtyFlag(this);
            }

            isDirty = false;
        }
    }

    public bool IsDirty
    {
        get { return isDirty; }
    }
}
