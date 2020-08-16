using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Objects/My hand")]
public class MyHand : ObjectLogic
{
    public GameEvent onCurrentCardSelected;
    public CardVariable currentCard;
    public State holdingCard;


    public override void OnClick(CardInstance inst)
    {
        currentCard.Set(inst);
        Settings.gameManager.SetState(holdingCard);
        Debug.Log("This card is in my hand.");      // if the card which is being clicked is in my hand this shows up.
        onCurrentCardSelected.Raise();
    }

    public override void OnHighlight(CardInstance inst)
    {

    }
}
