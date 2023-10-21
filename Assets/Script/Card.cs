using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public UnityAction<CardData> OnCardselected;
    public UnityAction<CardData, GameObject> OnCardUsed;
    public CardData cardData;

    private void Start()
    {
        if(cardData.sprite != null)
        {
            GetComponent<Image>().sprite = cardData.sprite;
        }
    }

    public void CardSelected()
    {
        OnCardselected.Invoke(cardData);
    }
}
