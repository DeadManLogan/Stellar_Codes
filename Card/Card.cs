using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    [System.NonSerialized]
    public int instId;
    public CardType cardType;
    public CardProperties[] properties;
    public int cost;
    [System.NonSerialized]
    public CardViz cardViz;

    public CardProperties GetProperty(Element e)
    {
        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].element = e)
            {
                return properties[i];
            }
        }

        return null;
    }
}


