using System.Collections;
using System.Collections.Generic;
using Sunflyer.Audio;
using UnityEngine;

public class HouseSelector : MonoBehaviour
{
    [SerializeField] private GameObjectVariable _houseSelected;
    [SerializeField] private AudioPlayer Hover;

    private void OnMouseEnter()
    {
        Hover.PlayAudio();
        _houseSelected.CurrentValue = this.gameObject;
    }

    private void OnMouseExit()
    {
        _houseSelected.CurrentValue = null;
    }
}
