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
            _card.OnCardselected += OnCardSelect;
            _card.OnCardUsed += OnCardUsed;
            _card.OnCardHovered += OnCardHover;

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
            if (target.TryGetComponent(out HouseManager house))
            {
                if (house.CurrentCard)
                {
                    if (WinManager.Instance.hasChampion() || _card)
                    {
                        return;
                    }

                    if (house.CurrentCard.defense > cardData.damage)
                    {
                        Lose.PlayAudio();
                    }
                    else
                    {
                        Win.PlayAudio();
                    }
                    return;
                }
            }
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
