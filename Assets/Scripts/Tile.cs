using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject occupyingPiece;

    public void BeOccupied(GameObject piece)
    {
        if (piece.GetComponent<BaseChessPiece>() != null)
        {
            occupyingPiece = piece;
        }
    }

    public GameObject GetOccupyingPiece { get { return occupyingPiece; } }
}
