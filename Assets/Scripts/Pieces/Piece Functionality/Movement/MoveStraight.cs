using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight
{
    public List<GameObject> StraightMoves(BaseChessPiece chessPiece)
    {
        List<GameObject> straight = new List<GameObject>();

        char column = chessPiece.currentTile.name[0];
        int row = chessPiece.currentTile.name[1] - '0';



        return straight;
    }
}
