using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

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

        /*if (currentTile != null)
        {
            rectTransform.SetParent(currentTile.transform);
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.SetParent(defaultParent);
        }*/
    }

    private void Start()
    {
        GameObject tile;
        tile = GameManager._Instance.FindTile(currentTile);

        GameManager._Instance.OccupyTile(this.gameObject);
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

            rectTransform.SetParent(FindObjectOfType<Canvas>().transform);

            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if (hit2D.collider == null)
        {
            return;
        }

        if (hit2D.collider.CompareTag("Tile") == true)
        {
            rectTransform.SetParent(hit2D.collider.transform);
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.SetParent(defaultParent);

            currentTile = hit2D.collider.gameObject;
        }
        else 
        {
            rectTransform.SetParent(currentTile.transform);
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.SetParent(defaultParent);
        }
    }
    /*
     * End of Interface Implementations
     */

    public void Capture(GameObject enemyPiece)
    {

    }

    public abstract void Move();
}
