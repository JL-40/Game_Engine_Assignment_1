using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public abstract class BaseChessPiece : MonoBehaviour, Command, IDragHandler//, IEndDragHandler
{
    [SerializeField] protected MovementType movementType; // The type of movement the piece can perform.
    [SerializeField, Range(1, 8)] protected int moveSteps;  // The number of tiles the piece can move.
    [SerializeField] protected PieceColor color;    // The color of the piece.

    [SerializeField] protected bool canMove = false; // Prevents players from moving pieces out of turn.
    [SerializeField] protected GameObject currentTile;

    protected RectTransform rectTransform;
    protected Invoker invoker;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PlayersTurn(bool turnEnds = false)
    {
        if (turnEnds && canMove)
        {
            canMove = false;
        }

        if (!canMove && !turnEnds)
        {
            canMove = true;
        }
    }

    public void Execute()
    {

    }

    /*
     * Interface Implementations
     */

    // Function that allows the player to drag and drop the piece.
    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

/*    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(currentTile.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {

        }
    }    */
    /*
     * End of Interface Implementations
     */

    public void Capture(GameObject enemyPiece)
    {

    }

    public abstract void Move();

    // Getter to check if the piece can move
    public bool CanMove {  get { return canMove; } }

    // Getter for other pieces to use to check if they are occupied.
    public GameObject CurrentTile { get { return currentTile; }
}
}
