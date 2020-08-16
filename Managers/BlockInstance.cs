using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInstance
{
    public CardInstance attacker;
    public List<CardInstance> blocker = new List<CardInstance>();
}
