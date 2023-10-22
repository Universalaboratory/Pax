using System;
using UnityEngine;
using UnityEngine.UI;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private GameObject cardInHouse;
    [SerializeField] private CardData currentCardData;
    [SerializeField] private SpriteRenderer sprHouse;
    private void Start()
    {
        sprHouse = GetComponentInChildren<SpriteRenderer>();
    }
    internal void handleNewAction(CardData card)
    {
       Debug.Log("Lidar com a ação");
       if(currentCardData == null)
       {
            //Não possui card
            //adiciona card aqui
            currentCardData = card;
            sprHouse.sprite = card.cardConfig.GetTile(gameObject.tag).Sprite;
       }
       else
       {
            //possui card
            //faz outra coisa
       }
    }
}
