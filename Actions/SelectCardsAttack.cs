using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Actions/Select Cards Attack")]
public class SelectCardsAttack : Action
{
    public override void Execute(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = Settings.GetUIObjs();        // that is a list that has every object we hit

            foreach (RaycastResult r in results)
            {
                CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();
                PlayerHolder p = Settings.gameManager.currentPlayer;
                
                if (!p.battlefieldCards.Contains(inst))
                {
                    return;
                }

                if (inst.CanAttack())
                {
                    p.attackingCards.Add(inst);
                    p.currentHolder.SetCardOnBattleLine(inst);
                }
            }
        }
    }
}
