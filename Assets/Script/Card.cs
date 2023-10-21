using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    public UnityAction<CardData> OnCardselected;
    
    public CardData cardData;

    public void CardSelected()
    {
        OnCardselected.Invoke(cardData);
    }
}
