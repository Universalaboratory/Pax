using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyAfterInstantiate : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
