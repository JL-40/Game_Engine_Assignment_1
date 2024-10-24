using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseChessPiece
{
    public bool isPromoted = false;

   public override void Move()
    {

    }

    /*
     * Function that promotes the pawn into other pieces excluding the King
     */
    public void Promote()
    {
        if (isPromoted == false)
        {
            isPromoted = true;
        }

        // Promote pawn.
    }
}
