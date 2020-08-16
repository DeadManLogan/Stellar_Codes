using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turns/Battle Phase Player")]
public class BattlePhase : Phase
{
    public State battlePhaseControl;
    public Condition isBattleValid;

    public override bool IsComplete()
    {
        if (forceExit)
        {
            forceExit = false;
            return true;
        }
        return false;
    }

    public override void OnStartPhase()
    {
        if (!isInit)
        { 
            //forceExit = !isBattleValid.IsValid();     // this lines has problems.skips every battle phase
            Settings.gameManager.SetState((!forceExit)? battlePhaseControl : null);
            Settings.gameManager.onPhaseChanged.Raise();
            isInit = true;
        }
    }

    public override void OnEndPhase()
    {
        if (isInit)
        {
            Settings.gameManager.SetState(null);
            isInit = false;
        }
    }
}
