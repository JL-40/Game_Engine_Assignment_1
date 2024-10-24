using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Straight,
    Diagonal,
    Any,
    Single,
    Angle
}

public abstract class BaseChessPiece : MonoBehaviour
{
    [SerializeField] protected MovementType movementType;

    [SerializeField, Range(1, 8)] protected int moveSteps;

    [SerializeField] protected PieceColor color;

    [SerializeField] protected bool isSelected = false;
    public void Capture()
    {

    }

    public void Move()
    {

    }
}
