using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    public abstract void Notify(Subject subject);
    public abstract void NotifyDirtyFlag(Subject subject);
}
