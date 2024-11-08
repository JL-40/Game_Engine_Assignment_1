using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : BaseChessPiece
{
    public override List<GameObject> ValidMove()
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

    public override void Execute()
    {

    }

    public override void Notify(Subject subject)
    {
        throw new System.NotImplementedException();
    }
}
