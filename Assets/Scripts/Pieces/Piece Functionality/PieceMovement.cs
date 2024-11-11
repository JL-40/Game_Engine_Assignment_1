using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : Command
{
    [SerializeField] GameObject piece;
    BaseChessPiece chessPiece;

    [SerializeField] RectTransform rectTransform;

    // Constructor
    public PieceMovement(GameObject gameObject)
    {
        piece = gameObject;
        chessPiece = piece.GetComponent<BaseChessPiece>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Execute()
    {
        Movement();
    }

    void Movement()
    {
        List<GameObject> validMoves = new List<GameObject>();

        validMoves = chessPiece.ValidMoves();

        // Do nothing if the piece cannot move
        if (!chessPiece.CanMove)
        {
            return;
        }

        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(piece.transform.position, Vector2.down, 1f); // Get an array of colliders hit

        foreach (RaycastHit2D hit in hit2Ds)
        {
            Collider2D hitCollider = hit.collider;
            GameObject hitObject = hit.collider.gameObject;

            // Ignores ray hitting itself
            if (hitObject.Equals(piece))
            {
                continue;
            }

            // Check if move is valid
            foreach (GameObject validTile in validMoves)
            {
                // Ignore non-valid Tiles
                if (hitCollider.name != validTile.name && hitCollider.GetComponent<BaseChessPiece>() == null)
                {
                    continue;
                }

                // Check if the piece is not the same color as this piece. Capture the piece.
                if (hitCollider.GetComponent<BaseChessPiece>() != null && hitCollider.GetComponent<BaseChessPiece>().ChessColor != chessPiece.ChessColor)
                {
                    chessPiece.Capture(hitObject); // Capture the piece

                    return;
                }

                // check if the piece moved to a tile.
                if (hitCollider != null && hitCollider.CompareTag("Tile") && hit2Ds.Length < 3)
                {
                    if (chessPiece.name.Contains("Pawn"))
                    {
                        string forwardTileName = $"{chessPiece.currentTile.name[0]}{(int)(chessPiece.currentTile.name[1] - '0') + 1}";
                        if (validTile.name != forwardTileName)
                        {
                            continue;
                        }
                    }

                    // Move to tile
                    SetTilePosition(hitObject);

                    chessPiece.currentTile = hitObject;

                    //PlayersTurn(false);

                    return;
                }
            }
        }

        // Reset position
        SetTilePosition(chessPiece.currentTile);
    }

    /// <summary>
    /// Resets the tile the piece was at. Cancels the movement of the piece.
    /// </summary>
    public void SetTilePosition(GameObject newParent = null)
    {
        rectTransform.SetParent(newParent.transform);
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.SetParent(GameObject.Find("Canvas").transform);
    }

    public RectTransform PieceRect { get { return rectTransform; } }
}
