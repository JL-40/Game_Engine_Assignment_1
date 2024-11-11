using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bishop : BaseChessPiece
{
    public override List<GameObject> ValidMoves()
    {
        List<GameObject> validTiles = new List<GameObject>();

        return validTiles;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        PieceMovement.Execute();
    }
}
