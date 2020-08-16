﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Resources Manager")]
public class ResourcesManager : ScriptableObject
{
    public Element typeElement;
    public Card[] allCards;
    Dictionary<string, Card> cardsDict = new Dictionary<string, Card>();

    public void Init()
    {
        cardsDict.Clear();
        for (int i = 0; i < allCards.Length; i++)
        {
            cardsDict.Add(allCards[i].name, allCards[i]);
        }
    }

    public Card GetCardInstance(string id)
    {
        Card originalCard = GetCard(id);
        if (originalCard == null)
        {
            return null;
        }

        Card newInst = Instantiate(originalCard);
        newInst.name = originalCard.name;       // change the name if we want to use it on another dict
        return newInst;
    }

    Card GetCard(string id)
    {
        Card result = null;
        cardsDict.TryGetValue(id, out result);
        return result;
    }
}
