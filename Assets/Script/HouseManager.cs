using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sunflyer.Audio;
using FMOD.Studio;
using System.Collections;

public class HouseManager : MonoBehaviour
{
    public CardData CurrentCard => currentCardData;
    [SerializeField] private GameObject cardInHouse;
    [SerializeField] private CardData currentCardData;
    [SerializeField] private SpriteRenderer sprHouse;
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private List<TextMeshProUGUI> Texts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI AttributeText;
    [SerializeField] private string WinMessage = "Won Against", LoseMessage = "Lost Against";
    [SerializeField] private Color WinColor = Color.green, LoseColor = Color.red;
    [SerializeField] private float AnimationDuration = .5f, FadeDuration = .1f;
    public AudioInstanceReference Use;
    [SerializeField] private AudioPlayer Win, Lose;

    private GameObject _soldiers;
    private CombatAudio _combatAudio;
    private WaitForEndOfFrame _wait;


    private void Start()
    {
        TryGetComponent(out _combatAudio);
        Use.SetupInstance();
        // sprHouse = GetComponentInChildren<SpriteRenderer>();
    }

    internal void handleNewAction(CardData card)
    {
        AttributeText.gameObject.SetActive(false);
        Use.SetParameterByName("Action", card.type);

        if (currentCardData == null)
        {
            SetNewOwner(card);
            StartCoroutine(PlayUseAudio(false));
            AttributeText.gameObject.SetActive(true);
        }
        else
        {

            if (_combatAudio)
                _combatAudio.ChangeCombatMusic();

            SetupAnimation(card);
            StartCoroutine(PlayUseAudio(true, card.damage > currentCardData.defense));

            if (card.damage > currentCardData.defense)
            {
                SetNewOwner(card);
            }
        }

    }

    private void SetNewOwner(CardData card)
    {
        currentCardData = card;
        sprHouse.sprite = card.cardConfig.GetTile(gameObject.tag).Sprite;

        if (_soldiers)
        {
            Destroy(_soldiers);
        }

        SetupUI(card);

        if (card.Soldiers)
        {
            _soldiers = Instantiate(card.Soldiers);
            _soldiers.transform.position = transform.position;
        }

    }

    private void SetupAnimation(CardData card)
    {
        if (gameObject.GetComponent<HouseSelector>().CanPlay)
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.DOFade(1, FadeDuration).OnComplete(() =>
            {
                CanvasGroup.DOFade(0, FadeDuration).SetDelay(AnimationDuration).OnComplete(() =>
                {
                    CanvasGroup.alpha = 0;
                    AttributeText.gameObject.SetActive(true);
                });
            });

            bool win = card.damage > currentCardData.defense;
            Texts[0].text = card.cardName;
            Texts[1].text = win ? WinMessage : LoseMessage;
            Texts[1].color = win ? WinColor : LoseColor;
            Texts[2].text = currentCardData.cardName;
        }
    }

    private void SetupUI(CardData card)
    {
        AttributeText.text = $"Defense: {card.defense}";
    }
    public CardData GetCardData()
    {
        return currentCardData;
    }

    private IEnumerator PlayUseAudio(bool fight, bool won = true)
    {
        yield return _wait;

        if (WinManager.Instance.hasChampion() || !WinManager.Instance.Hand.HasCard() ||
        WinManager.Instance.Hand.CardsAmount() == 1)
        {
            yield break;
        }

        if (fight)
        {
            if (won)
            {
                Win.PlayAudio();
            }
            else
            {
                Lose.PlayAudio();
            }

            yield break;
        }

        Use.PlayAudio();

    }
}
