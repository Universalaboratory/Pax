using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSelector : MonoBehaviour
{
    [SerializeField] private GameObjectVariable _houseSelected;
    private void OnMouseEnter()
    {
        _houseSelected.CurrentValue = this.gameObject;
    }

    private void OnMouseExit()
    {
        _houseSelected.CurrentValue = null;
    }
}
