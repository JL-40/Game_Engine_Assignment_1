using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseChessPiece
{
    public override List<GameObject> ValidMove()
    {
        List<GameObject> validTiles = new List<GameObject>();

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
