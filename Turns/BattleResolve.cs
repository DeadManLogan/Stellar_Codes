using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turns/Battle Resolve")]
public class BattleResolve : Phase
{
    public Element attackElement;
    public Element defenceElement;

    public override bool IsComplete()
    {
        PlayerHolder p = Settings.gameManager.currentPlayer;
        PlayerHolder e = Settings.gameManager.GetEnemyOf(p);

        if (p.attackingCards.Count == 0)
        {
            return true;
        }

        Dictionary<CardInstance, BlockInstance> blockDict = Settings.gameManager.GetBlockInstances();

        for (int i = 0; i < p.attackingCards.Count; i++)
        {
            CardInstance inst = p.attackingCards[i];
            Card c = inst.viz.card;
            CardProperties attack = c.GetProperty(attackElement);
            if (attack == null)
            {
                Debug.LogError("This card cant attack");
                continue;
            }
            
            int attackValue = attack.intValue;

            BlockInstance bi = GetBlockInstanceOfAttacker(inst, blockDict);
            if (bi != null)
            {
                for (int b = 0; b < bi.blocker.Count; b++)
                {
                    CardProperties def = c.GetProperty(defenceElement);
                    if (def == null)
                    {
                        Debug.LogWarning("you are trying to block with a card with  no defence element");
                        continue;
                    }

                    attackValue -= def.intValue;

                    if (def.intValue <= attackValue)
                    {
                        bi.blocker[b].CardInstanceToGraveyard();
                    }
                }
            }

            if (attackValue <= 0)
            {
                attackValue = 0;
                inst.CardInstanceToGraveyard();
            }

            p.DropCard(inst, false);
            p.currentHolder.SetCardDown(inst);
            e.DoDamage(attackValue);
        }

        Settings.gameManager.ClearBlockInstances();
        p.attackingCards.Clear();
        return true;
    }

    BlockInstance GetBlockInstanceOfAttacker(CardInstance attacker, Dictionary<CardInstance, BlockInstance> blockInstances)
    {
        BlockInstance r = null;
        blockInstances.TryGetValue(attacker, out r);
        return r;
    }

    public override void OnStartPhase()
    {

    }

    public override void OnEndPhase()
    {
        
    }
}
