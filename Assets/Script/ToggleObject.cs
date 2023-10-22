using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField] private GameObject Target;

    public void Toggle()
    {
        Target.SetActive(!Target.activeInHierarchy);
    }
}
