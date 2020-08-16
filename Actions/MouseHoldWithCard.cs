using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Actions/Mouse Hold With Card")]
public class MouseHoldWithCard : Action
{
    public CardVariable currentCard;
    public State playerControlState;
    public State playerBlockState;
    public GameEvent onPlayerControlState;
    public Phase blockPhase;

    public override void Execute(float d)
    {
        bool mouseIsDown = Input.GetMouseButton(0);

        if (!mouseIsDown)
        {
            GameManager gm = Settings.gameManager;
            List<RaycastResult> results = Settings.GetUIObjs();        // that is a list that has every object we hit

            if (gm.turns[gm.turnIndex].currentPhase.value != blockPhase)
            {
                foreach (RaycastResult r in results)
                {
                    Area a = r.gameObject.GetComponentInParent<Area>();
                    if (a != null)
                    {
                        a.OnDrop();     // here we release the card and drop it on the battlefield
                        break;
                    }
                }

                currentCard.value.gameObject.SetActive(true);
                currentCard.value = null;

                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
            }
            else
            {
                foreach (RaycastResult r in results)
                {
                    CardInstance c = r.gameObject.GetComponentInParent<CardInstance>();
                    if (c != null)
                    {
                        int count = 0;
                        bool block = c.CanBeBlocked(currentCard.value, ref count);

                        if (block)
                        {
                            Settings.SetCardForBlock(currentCard.value.transform, c.transform, count);
                        }
                        
                        currentCard.value.gameObject.SetActive(true);
                        currentCard.value = null;
                        onPlayerControlState.Raise();
                        Settings.gameManager.SetState(playerBlockState);
                        break;
                    }
                }
            }
            return;
        }
    }
}
