using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Types/Spell")]
public class SpellCard : CardType
{
    public override void OnSetType(CardViz viz)
    {
        //base.OnSetType(viz);      // dont know whats this doing but this line in magical girl script creates problems...

        viz.statsHolder.SetActive(false);       // spell cards don't have any stats so we disable the statsholder
    }
}