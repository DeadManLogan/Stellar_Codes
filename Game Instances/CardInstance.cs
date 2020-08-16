using UnityEngine;
using System.Collections;

public class CardInstance : MonoBehaviour, IClickable
{
    public PlayerHolder owner;
    public CardViz viz;
    public ObjectLogic currentLogic;

    public void Start()
    {
        viz = GetComponent<CardViz>();
    }

    public void CardInstanceToGraveyard()
    {
        Settings.gameManager.PutCardToGraveyard(this);
    }

    public bool CanBeBlocked(CardInstance block, ref int count)
    {
        bool result = owner.attackingCards.Contains(this);

        if (result && viz.card.cardType.canAttack)
        {
            result = true;

            if (result)
            {
                Settings.gameManager.AddBlockInstance(this, block, ref count);
            }

            return result;
        }
        else
        {
            return false;
        }
    }

    public bool CanAttack()
    {
        bool result = true;

        if (viz.card.cardType.TypeAllowsForAttack(this))
        {
           result = true; 
        }

        return result;
    }

    public void OnClick()
    {
        if (currentLogic == null)
        {
            return;
        }

        currentLogic.OnClick(this);
    }

    public void OnHighlight()
    {
        if (currentLogic == null)
        {
            return;
        }

        currentLogic.OnHighlight(this);
    }
}
