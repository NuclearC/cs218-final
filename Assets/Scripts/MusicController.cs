using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}