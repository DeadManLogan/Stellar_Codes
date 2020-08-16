using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSelectedCard : MonoBehaviour
{
    public CardVariable currentCard;
    public CardViz cardViz;

    Transform mTransform;

    public void LoadCard()
    {
        if (currentCard.value == null)
        {
            return;
        }

        currentCard.value.gameObject.SetActive(false);      // we move the card from our hand and place it to our mouse 
        cardViz.LoadCard(currentCard.value.viz.card);
        cardViz.gameObject.SetActive(true);
    }

    public void CloseCard()
    {
        cardViz.gameObject.SetActive(false);        // we dont want to have the card on our mouse when we start
    }

    void Start()
    {
        mTransform = this.transform;
        CloseCard();       
    }
    void Update()
    {
        mTransform.position = Input.mousePosition;
    }
}
