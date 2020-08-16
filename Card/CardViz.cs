using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;
using System.Security.Cryptography;

public class CardViz : MonoBehaviour
{

    public Card card;
    public CardVizProperties[] properties;
    public GameObject statsHolder;      // we store the stats of the card (attack & def)

    public void LoadCard(Card x)
    {
        if (x == null)
            return;

        x.cardViz = this;
        card = x;

        x.cardType.OnSetType(this);     // we pass the cardType on x

        CloseAll();

        for (int i = 0; i < x.properties.Length; i++)
        {
            CardProperties cp = x.properties[i];
            CardVizProperties p = GetProperty(cp.element);

            if (p == null)
                continue;

            if (cp.element is ElementText)
            {
                p.text.text = cp.stringValue;
                p.text.gameObject.SetActive(true);
            }
            else if (cp.element is ElementImage)
            {
                p.img.sprite = cp.sprite;
                p.img.gameObject.SetActive(true);
            }
            else if (cp.element is ElementInt)
            {
                p.text.text = cp.intValue.ToString();
                p.text.gameObject.SetActive(true);
            }
        }
    }


    public void CloseAll()
    {
        foreach (CardVizProperties p in properties)
        {
            if (p.img != null)
            {
                p.img.gameObject.SetActive(false);
            }

            if (p.text != null)
            {
                p.text.gameObject.SetActive(false);
            }
            
        }
    }

    public CardVizProperties GetProperty(Element e)
    {
        CardVizProperties result = null;

        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].element == e)
            {
                result = properties[i];
                break;
            }
        }
        return result;
    }
}

