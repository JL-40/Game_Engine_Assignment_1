using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseChessPiece : MonoBehaviour
{
    [SerializeField] protected MovementType movementType;
    [SerializeField, Range(1, 8)] protected int moveSteps;
    [SerializeField] protected PieceColor color;
    [SerializeField] protected Button button;

    [SerializeField] protected bool isSelected = false;
    [SerializeField] protected bool canMove = false;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Capture()
    {

    }

    public abstract void Move();
}
