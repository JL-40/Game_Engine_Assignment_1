using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    [SerializeField] GameObject _CurrentTile;
    public void ExecuteCommand(Command command)
    {
        command.Execute();


    }

    public GameObject CurrentTile { get { return _CurrentTile; } }
}
