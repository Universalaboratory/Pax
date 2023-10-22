using System.Collections;
using System.Collections.Generic;
using Sunflyer.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sunflyer.Audio
{
    public class SliderAudio : MonoBehaviour
    {
        [SerializeField] private string BusPath;
        [SerializeField] private TextMeshProUGUI Value, Label;
        private Slider _slider;

        private void Start()
        {
            TryGetComponent(out _slider);
            _slider.onValueChanged.AddListener(OnValueChanged);
            _slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume(BusPath));
            Label.text = BusPath;
            UpdateText();
        }

        private void OnValueChanged(float value)
        {
            AudioManager.Instance.ChangeVolume(BusPath, value);
            UpdateText();
        }

        private void UpdateText()
        {
            Value.text = (_slider.value * 100).ToString("0");
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }
    }
}
