using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turns/Block Phase")]
public class BlockPhase : Phase
{
    public PlayerAction changeActivePlayer;
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
            GameManager gm = Settings.gameManager;
            gm.SetState(playerControlState);
            gm.onPhaseChanged.Raise();
            isInit = true;

            if (gm.currentPlayer.attackingCards.Count == 0)
            {
                forceExit = true;
                return;
            }

            if (gm.otherPlayerHolder.playerHolder.isHumanPlayer)
            {
                gm.LoadPlayerOnActive(gm.otherPlayerHolder.playerHolder);
            }
            else
            {

            }
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
