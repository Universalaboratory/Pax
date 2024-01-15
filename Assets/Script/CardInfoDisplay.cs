using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoDisplay : MonoBehaviour
{
    private CardData cardData;
    private Card card;
    private TextMeshProUGUI cardText;
    [SerializeField] private Image cardImage;

    private void Awake()
    {
        cardText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        card = GetComponent<Card>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        //cardText.text = $"{card.cardData.cardName} \nAtack: {card.cardData.damage} \nDefense: {card.cardData.defense}";
        cardText.text = "";
        cardImage.sprite = card.cardData.sprite;
    }
}