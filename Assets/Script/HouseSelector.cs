using System.Collections;
using System.Collections.Generic;
using Sunflyer.Audio;
using UnityEngine;

public class HouseSelector : MonoBehaviour
{
    [SerializeField] private GameObjectVariable _houseSelected;
    [SerializeField] private AudioPlayer Hover;

    public bool CanPlay
    {
        get { return _canPlay; }
        set { _canPlay = value; }
    }

    [SerializeField] private bool _canPlay;

    private void OnMouseEnter()
    {
        if (_canPlay)
        {
            Hover.PlayAudio();
            _houseSelected.CurrentValue = this.gameObject;
        }
    }

    private void OnMouseExit()
    {
        if (_canPlay)
        {
            _houseSelected.CurrentValue = null;
        }
    }
}
