using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    public Canvas canvas;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }
}
