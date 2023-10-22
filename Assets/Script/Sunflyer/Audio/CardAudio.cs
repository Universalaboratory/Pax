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
        [SerializeField] private AudioPlayer Hover, Upgrade, Return;
        private Card _card;

        private void Awake()
        {
            TryGetComponent(out _card);
            _card.OnCardselected += OnCardSelect;
            _card.OnCardUsed += OnCardUsed;
            _card.OnCardHovered += OnCardHover;
        }

        private void Start()
        {
            Select.SetupInstance();
            Use.SetupInstance();
            Select.SetParameterByName(Parameter, _card.cardData.type);
            Use.SetParameterByName(Parameter, _card.cardData.type);
        }

        private void OnCardHover(Card card)
        {
            Hover.PlayAudio();
        }

        private void OnCardUsed(CardData cardData, GameObject target)
        {
            Use.PlayAudio();
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
            _card.OnCardUsed -= OnCardUsed;
            _card.OnCardHovered -= OnCardHover;
            AudioManager.Instance?.ReleaseInstance(Select.AudioInstance);
            AudioManager.Instance?.ReleaseInstance(Use.AudioInstance);
        }
    }

}