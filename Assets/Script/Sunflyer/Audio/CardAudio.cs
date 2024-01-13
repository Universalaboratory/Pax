using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sunflyer.Audio
{
    public class CardAudio : MonoBehaviour
    {
        [SerializeField] private string Parameter = "Action";
        [SerializeField] private AudioInstanceReference Select, Use;
        [SerializeField] private AudioPlayer Hover, Upgrade, Return, Win, Lose;
        private Card _card;

        private void Start()
        {
            TryGetComponent(out _card);

            if (!_card)
            {
                _card = GetComponentInParent<Card>();
                transform.parent = null;
            }

            _card.OnCardselected += OnCardSelect;
           // _card.OnCardUsed += OnCardUsed;
            _card.OnCardHovered += OnCardHover;
            _card.OnReceiveCardData += OnReceiveCardData;

            Select.SetupInstance();
            Use.SetupInstance();

        }

        private void OnReceiveCardData(CardData newCardData)
        {
            Select.SetParameterByName(Parameter, _card.cardData.type);
            Use.SetParameterByName(Parameter, _card.cardData.type);
        }

        private void OnCardHover(Card card)
        {
            Hover.PlayAudio();
        }

        private void OnCardUsed(CardData cardData, GameObject target)
        {
            if (target.TryGetComponent(out HouseManager house))
            {
                Debug.Log($"SETUP NEW CARD { _card.cardData.type}");
               // house.Use.SetParameterByName(Parameter, _card.cardData.type);
            }
            //  StartCoroutine(PlayUseAudio(cardData, target));
        }

        public void OnCardReturn()
        {
            Return.PlayAudio();
        }

        private void OnCardSelect(CardData cardData)
        {
            Select.PlayAudio();
        }

        private void OnDestroy()
        {
            _card.OnCardselected -= OnCardSelect;
           // _card.OnCardUsed -= OnCardUsed;
            _card.OnCardHovered -= OnCardHover;
            _card.OnReceiveCardData -= OnReceiveCardData;

            AudioManager.Instance?.ReleaseInstance(Select.AudioInstance);
            AudioManager.Instance?.ReleaseInstance(Use.AudioInstance);
        }
    }

}
