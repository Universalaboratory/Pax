using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public UnityAction<CardData> OnCardselected;
    public UnityAction<CardData, GameObject> OnCardUsed;
    public UnityAction<Card> OnCardHovered;
    public UnityAction<CardData> OnReceiveCardData;
    public CardData cardData;
    public CardInfoDisplay info;

    private void Start()
    {
        if (cardData.sprite != null)
        {
            GetComponent<Image>().sprite = cardData.sprite;
        }
    }

    public void ReceiveCardData(CardData newCardData)
    {
        cardData = newCardData;
        UpdateUI();
        OnReceiveCardData?.Invoke(newCardData);
    }

    public void UpdateUI()
    {
        info.UpdateUI();
    }

    public void CardSelected()
    {
        OnCardselected.Invoke(cardData);
    }
}
