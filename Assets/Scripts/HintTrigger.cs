
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] int hintIndex = -1;

    private Hints hints;

    void Start()
    {
        hints = FindObjectOfType<Hints>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (hints && hintIndex >= 0 && other.CompareTag("Player"))
            hints.ShowHint(hintIndex);
    }
}