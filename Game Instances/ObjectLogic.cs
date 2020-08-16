using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectLogic : ScriptableObject
{
    public abstract void OnClick(CardInstance inst);
    public abstract void OnHighlight(CardInstance inst);
}
