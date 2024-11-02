using System.Collections;
using System.Collections.Generic;
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
        defaultParent = rectTransform.parent;
    }

    private void Start()
    {
        GameObject tile;
        tile = GameManager._Instance.FindTile(currentTile);
        
        if (tile.GetComponent<Tile>() != null)
        {
            tile.GetComponent<Tile>().BeOccupied(this.gameObject);
        }
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

        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);

        foreach (RaycastHit2D hit2D in hit2Ds)
        {
            Debug.Log(hit2D.collider ? hit2D.collider.name : hit2D.collider);

            // check if the piece moved to a tile.
            if (hit2D.collider == null || !hit2D.collider.CompareTag("Tile"))
            {
                rectTransform.SetParent(currentTile.transform);
                rectTransform.anchoredPosition = Vector3.zero;
                rectTransform.SetParent(defaultParent);
            }
            else
            {
                rectTransform.SetParent(hit2D.collider.transform);
                rectTransform.anchoredPosition = Vector3.zero;
                rectTransform.SetParent(defaultParent);

                currentTile = hit2D.collider.gameObject;
            }
        }
    }

    // End of Interface Implementations
    
    public void Capture(GameObject enemyPiece)
    {
        if (enemyPiece.GetComponent<BaseChessPiece>() != null && enemyPiece.GetComponent<BaseChessPiece>().color != this.color)
        {

        }
    }

    public abstract void Move();
}
