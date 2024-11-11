using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pawn : BaseChessPiece
{
    public bool isPromoted = false;
    [SerializeField] List<GameObject> promotionList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        pieceMovement = new PieceMovement(this.gameObject);
    }

    /// <summary>
    /// Function that promotes the pawn into other pieces excluding the King
    /// </summary>
    public void Promote()
    {
        if (isPromoted == false)
        {
            isPromoted = true;
        }

        GameObject promotedPawn = null;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Promote to Rook
        {
            promotedPawn = Instantiate(promotionList[0]);
            promotedPawn.name = $"{promotionList[0].name}";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Promote to Knight
        {
            promotedPawn = Instantiate(promotionList[1]);
            promotedPawn.name = $"{promotionList[1].name}";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Promote to Bishop
        {
            promotedPawn = Instantiate(promotionList[2]);
            promotedPawn.name = $"{promotionList[2].name}";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //Promote to Queen
        {
            promotedPawn = Instantiate(promotionList[3]);
            promotedPawn.name = $"{promotionList[3].name}";
        }

        // Error, no promoted pawn
        if (promotedPawn == null)
        {
            return;
        }

        promotedPawn.name += " (Promoted)";

        BaseChessPiece promotedPawnBase = promotedPawn.GetComponent<BaseChessPiece>();
        promotedPawnBase.currentTile = this.currentTile;
        promotedPawnBase.PieceMovement.SetTilePosition(currentTile);
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
            validTileNames.Add($"{(char)(currLetter-1)}{currNumber + 1}");
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

    public override void Notify(Subject subject)
    {
        throw new System.NotImplementedException();
    }
}
