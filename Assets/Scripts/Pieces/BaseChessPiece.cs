using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseChessPiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] protected MovementType movementType; // The type of movement the piece can perform.
    [SerializeField, Range(1, 8)] protected int moveSteps;  // The number of tiles the piece can move.
    [SerializeField] protected PieceColor color;    // The color of the piece.

    [SerializeField] public bool canMove = false; // Prevents players from moving pieces out of turn.
    [SerializeField] public GameObject currentTile;

    [SerializeField] protected PieceMovement pieceMovement;

    protected virtual void Awake()
    {
        if (color == PieceColor.White && !this.CompareTag("White"))
        {
            this.tag = "White";
        }
        else if (color == PieceColor.Black && !this.CompareTag("Black"))
        {
            this.tag = "Black";
        }
    }

    private void Start()
    {
        if (currentTile == null)
        {
            throw new System.Exception($"Missing Current Tile on {gameObject.name}.");
        }
    }

    public void PlayersTurn(bool dontEndTurn = true)
    {
        if (!dontEndTurn)
        {
            canMove = false;
            return;
        }
        canMove = true;
    }

    // GETTERS
    public PieceColor ChessColor { get { return color; } }
    public bool CanMove { get { return canMove; } }

    public PieceMovement PieceMovement { get { return pieceMovement; } }

    // MOVE AND CAPTURE
    public virtual void Capture(GameObject enemyPiece)
    {
        // Double Check before capturing
        if (enemyPiece.GetComponent<BaseChessPiece>() != null && enemyPiece.GetComponent<BaseChessPiece>().ChessColor != this.color)
        {
            // Move to new tile
            pieceMovement.SetTilePosition(enemyPiece.GetComponent<BaseChessPiece>().currentTile);

            currentTile = enemyPiece.GetComponent<BaseChessPiece>().currentTile;

            Destroy(enemyPiece); // Remove enemy piece
        }
        else
        {
            pieceMovement.SetTilePosition(currentTile);
        }
    }

    public abstract List<GameObject> ValidMoves();

    // PLAYER INTERACTION
    /// <summary>
    /// Function that allows the player to drag the piece.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (CanMove)
        {
            pieceMovement.PieceRect.anchoredPosition += eventData.delta;
        }
    }

    /// <summary>
    /// Function that allows the player to place the piece
    /// </summary>
    /// <param name="eventData"></param>
    public abstract void OnEndDrag(PointerEventData eventData);
}
