using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseChessPiece
{
    public bool isPromoted = false;

   public override void Move()
    {
        
    }

    public void Promote()
    {
        if (isPromoted == false)
        {
            isPromoted = true;
        }

        // Promote pawn.
    }

    public void Selected()
    {
        if (canMove == false)
        {
            return;
        }

        isSelected = (isSelected == false ? true : false);

        if (isSelected)
        {
           Move();
        }
    }
}
