using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseChessPiece : MonoBehaviour, Command, IDragHandler, IEndDragHandler
{
    [SerializeField] protected MovementType movementType; // The type of movement the piece can perform.
    [SerializeField, Range(1, 8)] protected int moveSteps;  // The number of tiles the piece can move.
    [SerializeField] protected PieceColor color;    // The color of the piece.

    [SerializeField] public bool canMove = false; // Prevents players from moving pieces out of turn.
    [SerializeField] public GameObject currentTile;

    protected RectTransform rectTransform;
    Transform defaultParent;

    protected Invoker invoker;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        defaultParent = GameObject.Find("Canvas").transform;

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

    public void Execute()
    {

    }

    
    // Interface Implementations
    
    // Function that allows the player to drag and drop the piece.
    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Do nothing if the piece cannot move
        if (!canMove)
        {
            return;
        }

        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(transform.position, Vector2.down, 1f); // Get an array of colliders hit

        foreach (RaycastHit2D hit in hit2Ds)
        {
            if (hit.collider.gameObject.Equals(this.gameObject))
            {
                continue;
            }

            // Check if the piece is not the same color as this piece. Capture the piece.
            if (hit.collider.GetComponent<BaseChessPiece>() != null && hit.collider.GetComponent<BaseChessPiece>().Color != color)
            {
                Capture(hit.collider.gameObject); // Capture the piece

                //PlayersTurn(false);

                return;
            }

            // check if the piece moved to a tile.
            if (hit.collider != null && hit.collider.CompareTag("Tile") && hit2Ds.Length < 3)
            {
                // Move to tile
                rectTransform.SetParent(hit.collider.transform);
                rectTransform.anchoredPosition = Vector3.zero;
                rectTransform.SetParent(defaultParent);

                currentTile = hit.collider.gameObject;

                //PlayersTurn(false);

                return;
            }
        }

        ResetTilePosition();
    }

    // End of Interface Implementations
    
    public void Capture(GameObject enemyPiece)
    {
        // Double Check before capturing
        if (enemyPiece.GetComponent<BaseChessPiece>() != null && enemyPiece.GetComponent<BaseChessPiece>().Color != this.color)
        {
            // Move to new tile
            rectTransform.SetParent(enemyPiece.GetComponent<BaseChessPiece>().currentTile.transform);
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.SetParent(defaultParent);

            currentTile = enemyPiece.GetComponent<BaseChessPiece>().currentTile;

            Destroy(enemyPiece); // Remove enemy piece
        }
        else
        {
            ResetTilePosition();
        }
    }

    public abstract void Move();

    /// <summary>
    /// Resets the tile the piece was at. Cancels the movement of the piece.
    /// </summary>
    void ResetTilePosition()
    {
        // Reset position
        rectTransform.SetParent(currentTile.transform);
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.SetParent(defaultParent);
    }

    public PieceColor Color {  get { return color; } }
}
