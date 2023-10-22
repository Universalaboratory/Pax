using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sunflyer.Audio
{
    public class ButtonAudioPlayer : MonoBehaviour, IPointerEnterHandler
    {
        public AudioPlayer ClickAudio, HoverAudio;
        protected Button _button;

        protected virtual void Awake()
        {
            TryGetComponent(out _button);

            _button.onClick.AddListener(Click);
        }

        protected virtual void Click()
        {
            ClickAudio.PlayAudio();
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(Click);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                HoverAudio.PlayAudio();
            }
        }
    }
}