using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject
{
    public abstract void SubscribeToSubject(Observer observer);

    public abstract void UnsubscribeToSubject(Observer observer);

    public abstract void NotifyObservers();
}
