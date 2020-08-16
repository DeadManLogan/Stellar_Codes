using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Actions/On Select Card")]
public class OnSelectCard : Action
{
    public GameEvent onCurrentCardSelected;
    public CardVariable currentCard;
    public State holdingCard;

    public override void Execute(float d)       //d is for delta time
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = Settings.GetUIObjs();        // that is a list that has every object we hit

            foreach (RaycastResult r in results)
            {
                CardInstance c = r.gameObject.GetComponentInParent<CardInstance>();
                if (c != null)
                {
                    GameManager gm = Settings.gameManager;
                    PlayerHolder enemy = gm.GetEnemyOf(gm.currentPlayer);

                    if (c.owner == enemy)
                    {
                        currentCard.value = c;
                        gm.SetState(holdingCard);
                        onCurrentCardSelected.Raise();
                    }

                    return;
                }
            }
        }
    }
}
