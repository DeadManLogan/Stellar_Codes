using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Player Actions/Pick Card From Deck")]
public class PickCardFromDeck : PlayerAction
{
    public override void Execute(PlayerHolder player)
    {
        GameManager.singleton.PickNewCardFromDeck(player);
    }
}
