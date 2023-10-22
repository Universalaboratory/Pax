using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "GMJP/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int damage;
    public int defense;
    public int type;
    public int cardId;
    public Sprite sprite;
    public Sprite[] cardSprites;
    public CardConfig cardConfig;

    public void LoadCardSprite()
    {
        try
        {
            sprite = cardSprites[cardId];
        }
        catch(Exception e)
        {
            //ignora
        }
    }

    public override string ToString()
    {
        return "nome: " + cardName + "\ndamage: " + damage + "\ndefense: " + defense + "\ntype: " + type;
    }
}
