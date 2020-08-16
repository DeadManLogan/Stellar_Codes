using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turns/Control Phase Player")]
public class ControlPhasePlayer : Phase
{
    public State playerControlState;

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
            Settings.gameManager.SetState(playerControlState);
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
