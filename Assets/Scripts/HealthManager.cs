using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int health = 100;

    public UnityEvent<int, int> OnHealthChange { get; private set; }

    void Awake()
    {
        OnHealthChange = new UnityEvent<int, int>();
    }
    public void SetHealth(int h)
    {
        health = h;
    }
    public void AddHealth(int d)
    {
        health = Math.Clamp(health + d, 0, 100);
        OnHealthChange.Invoke(health, d);
    }

    public int Value { get { return health; } }
}
