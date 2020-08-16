using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phase : ScriptableObject
{
    public string phaseName;
    public bool forceExit;

    public abstract bool IsComplete();

    [System.NonSerialized]
    protected bool isInit;

    public abstract void OnStartPhase();

    public abstract void OnEndPhase();
}
