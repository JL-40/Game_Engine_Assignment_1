using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseChessPiece
{
    public bool isPromoted = false;
    [SerializeField] List<GameObject> promotionList = new List<GameObject>();


   public override List<GameObject> ValidMove()
    {
        List<GameObject> validTiles = new List<GameObject>();

        string tileLetter = $"{currentTile.name[0]}";

        foreach (Coordinate coors in GameManager._Instance.GetTiles)
        {
            foreach (GameObject tile in coors._Tiles)
            {
                if (validTiles.Count == 1)
                {
                    return validTiles;
                }

                if (tile.name.Contains(tileLetter) && int.Parse($"{tile.name[1]}") > int.Parse($"{currentTile.name[1]}"))
                {
                    validTiles.Add(tile);
                }
            }
        }

        return validTiles;
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
        promotedPawnBase.ResetTilePosition();
    }

    /// <summary>
    /// Execute Promotion
    /// </summary>
    public override void Execute()
    {
        Promote();
    }

    public override void Notify(Subject subject)
    {
        throw new System.NotImplementedException();
    }
}
