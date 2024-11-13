using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rook : BaseChessPiece
{
    protected override void Awake()
    {
        base.Awake();

        pieceMovement = new PieceMovement(this.gameObject);
    }

    public override List<GameObject> ValidMoves()
    {
        List<GameObject> validTiles = new List<GameObject>();

        string tileLetter = $"{currentTile.name[0]}";
        string tileNumber = $"{currentTile.name[1]}";

        foreach (Coordinate coors in GameManager._Instance.GetTiles)
        {
            foreach (GameObject tile in coors._Tiles)
            {

                if (tile.name.Contains(tileLetter) || tile.name.Contains(tileNumber))
                {
                    validTiles.Add(tile);
                }
            }
        }

        return validTiles;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        PieceMovement.Execute();
    }
}
