using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/BattlePhaseStartCheck")]
public class BattlePhaseStartCheck : Condition
{
    public override bool IsValid()
    {
        GameManager gm = GameManager.singleton;

        if (gm.currentPlayer.battlefieldCards.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
