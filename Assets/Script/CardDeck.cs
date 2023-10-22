using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "GMJP/CardDeck")]
public class CardDeck : ScriptableObject
{
    public List<CardData> Deck = new List<CardData>();
    public string DeckName;
    public Sprite DeckImage;
}
