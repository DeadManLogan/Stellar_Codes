using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Types/Magical Girl")]
public class MagicalGirl : CardType
{
    public override void OnSetType(CardViz viz)
    {   
        base.OnSetType(viz);        // this line might need to be in comment
        viz.statsHolder.SetActive(true);        // magical girl cards need stats
    }
}
