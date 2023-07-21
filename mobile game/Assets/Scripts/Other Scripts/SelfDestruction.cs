using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] float timeUntilDestroy = 2f;

    void Start()
    {
        // Invoke the Destroy method after the specified time
        Invoke("Destroy", timeUntilDestroy);
    }

    void Destroy()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}
