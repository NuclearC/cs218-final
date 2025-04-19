
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
    [SerializeField] int hazardIndex = -1;

    private Hints hints;

    void Start()
    {
        hints = FindObjectOfType<Hints>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (hints && hazardIndex >= 0 && other.CompareTag("Player"))
            hints.ShowHazard(hazardIndex);
    }
}