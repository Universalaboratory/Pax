using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardHand : MonoBehaviour
{
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
        CardDeck = deck;
        _deck = new Queue<CardData>();
        foreach (var card in CardDeck.Deck)
        {
            _deck.Enqueue(card);
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].cardData = _deck.Peek();
            if (Cards[i].TryGetComponent(out CardMover mover))
            {
                mover.SetDeck(deck);
            }
            _deck.Dequeue();
        }
    }

    public void DrawCard()
    {
        var card = Cards.Find(c => !c.gameObject.activeInHierarchy);
        if (card)
        {
            Debug.Log("Has Card Disabled");
            if (_deck.Count > 0)
            {
                card.cardData = _deck.Peek();
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
                Debug.Log("FINISHED");
                OnDeckFinished?.Invoke();
            }
        }

    }

}

[System.Serializable]
public class CardConjunct
{
    public Card Card;
}
