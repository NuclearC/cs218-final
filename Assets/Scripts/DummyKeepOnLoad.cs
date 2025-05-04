using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DummyKeepOnLoad : MonoBehaviour
{
    [SerializeField] string singletonName = "";
    void Start()
    {
        var instances = GameObject.FindGameObjectsWithTag(singletonName);
        if (instances.Count() > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
