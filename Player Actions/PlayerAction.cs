using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is created in tutorial 20. migh not be needed
public abstract class PlayerAction : ScriptableObject
{
    public abstract void Execute(PlayerHolder player);
}
