using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "GMJP/CardLibrary")]
public class CardLibrary : ScriptableObject
{
    public List<CardData> cards;
    public List<CardDeck> Decks;
}
