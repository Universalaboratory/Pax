using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoDisplay : MonoBehaviour
{
    private CardData cardData;

    private TextMeshProUGUI cardText;
    private Image cardImage;

    private void Awake()
    {
        cardText = GetComponentInChildren<TextMeshProUGUI>();
        cardImage = GetComponent<Image>();
    }

    private void Start()
    {
        cardData = GetComponent<Card>().cardData;

        cardText.text = $"{cardData.name} \nAtaque: {cardData.damage} \nDefesa: {cardData.defense}";
        cardImage.sprite = cardData.sprite;
    }
}