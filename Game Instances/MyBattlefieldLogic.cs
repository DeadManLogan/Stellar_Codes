using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Areas/My Battlefield When Holding Card")]
public class MyBattlefieldLogic : AreaLogic
{
    public CardVariable card;       // the card we are using aka holding with our mouse
    public CardType creatureType;       // the card type(creature or spell).if it is a spell it can't be placed down on the battlefield
    public TransformVariable areaGrid;
    public ObjectLogic battlefieldLogic;

    public override void Execute()
    {
        if (card.value == null)
        {
            return;     // we don't want it if it's null
        }

        Card c = card.value.viz.card;

        if (c.cardType == creatureType)       // check if we have creature
        {
            bool canUse = Settings.gameManager.currentPlayer.CanUseCard(c);

            if (canUse)
            {
                Settings.DropCreatureCard(card.value.transform, areaGrid.value.transform, c);
                card.value.currentLogic = battlefieldLogic;
            }
            card.value.gameObject.SetActive(true);
            //Place card down

        }
    }
}
