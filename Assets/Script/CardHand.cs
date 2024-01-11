using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DeckAmount;
    [SerializeField] private UnityEngine.UI.Image DeckImage;
    public CardDeck CardDeck;
    public List<Card> Cards;
    private Queue<CardData> _deck;
    public System.Action OnDeckFinished;

    private void Awake()
    {
        GetCards();
    }

    private void GetCards()
    {
        Cards = FindObjectsOfType<Card>().ToList();
    }

    public void ReceiveDeck(CardDeck deck)
    {
        DeckImage.sprite = deck.DeckImage;
        CardDeck = deck;
        _deck = new Queue<CardData>();
        foreach (var card in CardDeck.Deck)
        {
            _deck.Enqueue(card);
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].ReceiveCardData(_deck.Peek());
            if (Cards[i].TryGetComponent(out CardMover mover))
            {
                mover.SetDeck(deck);
            }
            _deck.Dequeue();
        }
        UpadteDeckAmount();
        DeckImage.gameObject.SetActive(true);
    }

    private void UpadteDeckAmount()
    {
        DeckAmount.text = $"{CardDeck.DeckName} \nDeck: {_deck.Count}";
    }

    public void DrawCard()
    {
        var card = Cards.Find(c => !c.gameObject.activeInHierarchy);
        if (card)
        {
            if (_deck.Count > 0)
            {
                card.ReceiveCardData(_deck.Peek());
                _deck.Dequeue();
                card.gameObject.SetActive(true);
            }
            else
            {
                foreach (var c in Cards)
                {
                    if (c.gameObject.activeInHierarchy)
                    {
                        return;
                    }
                }

                OnDeckFinished?.Invoke();
            }
            UpadteDeckAmount();
        }

    }

    public bool HasCard()
    {
        foreach (var c in Cards)
        {
            if (c.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    public int CardsAmount()
    {
        int cardAmount = 0;
        foreach (var c in Cards)
        {
            if (c.gameObject.activeInHierarchy)
            {
                cardAmount++;
            }
        }
        return cardAmount;
    }
}

[System.Serializable]
public class CardConjunct
{
    public Card Card;
}
