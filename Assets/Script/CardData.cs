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
    public Sprite sprite;
    public CardConfig cardConfig;
    public GameObject Soldiers;

    public override string ToString()
    {
        return "nome: " + cardName + "\ndamage: " + damage + "\ndefense: " + defense + "\ntype: " + type;
    }
}
