using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardConfig", menuName = "GMJP/CardConfig")]
public class CardConfig : ScriptableObject
{
    public List<TileSpriteSetup> Tiles;

    public TileSpriteSetup GetTile(string tag)
    {
        return Tiles.Find(t => t.TileTag == tag);
    }
}


[System.Serializable]
public class TileSpriteSetup
{
    public Sprite Sprite;
    public string TileTag;
}