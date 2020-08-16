using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Holders/Card Holder")]
public class CardHolder : ScriptableObject
{
    public TransformVariable handGrid;
    public TransformVariable battlefieldGrid;
    public TransformVariable battleLine;

    [System.NonSerialized]
    public PlayerHolder playerHolder;

    public void SetCardDown(CardInstance card)
    {
        Settings.SetParentForCard(card.viz.gameObject.transform, battlefieldGrid.value.transform);
    }

    public void SetCardOnBattleLine(CardInstance card)
    {
        Vector3 position = card.viz.gameObject.transform.position;

        Settings.SetParentForCard(card.viz.gameObject.transform, battleLine.value.transform);
        position.z = card.viz.gameObject.transform.position.z;
        position.y = card.viz.gameObject.transform.position.y;
        card.viz.gameObject.transform.position = position;
    }

    public void LoadPlayer(PlayerHolder p, PlayerStatsUI statsUI)
    {
        if (p == null)
        {
            return;
        }

        playerHolder = p;
        p.currentHolder = this;

        foreach (CardInstance c in p.battlefieldCards)
        {
            if (!p.attackingCards.Contains(c))
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, battlefieldGrid.value.transform);
                //c.viz.gameObject.transform.SetParent(battlefieldGrid.value.transform); this line is replaced from the above in ep 17 for switch players
            }
            
        }

        foreach (CardInstance c in p.handCards)
        {
            if (c.viz != null)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value.transform);
                //c.viz.gameObject.transform.SetParent(handGrid.value.transform); this line is replaced from the above in ep 17 for switch players
            }
        }

        foreach (CardInstance c in p.attackingCards)
        {
            SetCardOnBattleLine(c);
        }

        p.statsUI = statsUI;
        p.LoadPlayerOnStatsUI();
    }
}
