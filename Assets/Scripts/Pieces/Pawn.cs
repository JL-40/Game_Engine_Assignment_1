using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseChessPiece
{
    public bool isPromoted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Promote()
    {
        if (isPromoted == false)
        {
            isPromoted = true;
        }

        // Promote pawn.
    }
}
