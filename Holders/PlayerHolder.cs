using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Holders/Player Holder")]
public class PlayerHolder : ScriptableObject
{
    public string username;
    public Sprite portrait;
    [System.NonSerialized]
    public int health = 20;
    public PlayerStatsUI statsUI;

    //public string[] startingCards;
    public List<string> startingDeck = new List<string>();
    [System.NonSerialized]
    public List<string> all_cards = new List<string>();

    public Color playerColor;

    public bool isHumanPlayer;

    [System.NonSerialized]
    public CardHolder currentHolder;        // this helps us use the hand and battlefield grid from card holder

    public ObjectLogic handLogic;
    public ObjectLogic battlefieldLogic;


    [System.NonSerialized]
    public int cardsPlayedThisTurn;
    public int playerLevel;

    // the lists are soft data which means we cannot count how many cards they will have
    [System.NonSerialized]
    public List<CardInstance> handCards = new List<CardInstance>();     // list with the cards the player holds in his hand
    [System.NonSerialized]
    public List<CardInstance> battlefieldCards = new List<CardInstance>();      // list with the cards the player has on his battlefield
    [System.NonSerialized]
    public List<CardInstance> attackingCards = new List<CardInstance>();

    public void Init()
    {
        health = 20;
        all_cards.AddRange(startingDeck);
    }

    public void CardToGraveyard(CardInstance c)
    {
        if (attackingCards.Contains(c))
        {
            attackingCards.Remove(c);
        }
        if (handCards.Contains(c))
        {
            handCards.Remove(c);
        }
        if (battlefieldCards.Contains(c))
        {
            battlefieldCards.Remove(c);
        }
    }

    public bool CanUseCard(Card c)
    {

        if(cardsPlayedThisTurn >= 1){
            Debug.Log("You've already played a card buddy");
            return false;		
        }
        if(playerLevel < c.cost){
            Debug.Log("This card is too strong for you buddy");
            return false;
        }

		return true;
	}

    public void DropCard(CardInstance inst, bool registerEvent = true)     // this whole methood is from ep 17 for switch player
    {
        if (handCards.Contains(inst))
        {
            handCards.Remove(inst);
        }

        battlefieldCards.Add(inst);

        if (registerEvent)
        {
            Settings.RegisterEvent(username + " used " + inst.viz.card.name, Color.white);
        }
    }

    public void DoDamage(int v)
    {
        health -= v;

        if (statsUI != null)
        {
            statsUI.UpdateHealth();
        }
    }
    
    public void LoadPlayerOnStatsUI()
    {
        if (statsUI != null)
        {
            statsUI.player = this;
            statsUI.UpdateAll();
        }
    }
}
