using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "GMJP/CardData")]
public class CardData : ScriptableObject
{
    public string name;
    public int damage;
    public int defense;
    public int type;
    public Sprite sprite;
}
