using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    public void ExecuteCommand(Command command)
    {
        command.Execute();
    }
}
