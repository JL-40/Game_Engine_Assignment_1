using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : BaseChessPiece
{
    public override List<GameObject> ValidMoves()
    {
        List<GameObject> validTiles = new List<GameObject>();

        return validTiles;
    }
    public override void Notify(Subject subject)
    {
        throw new System.NotImplementedException();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        PieceMovement.Execute();
    }
}
