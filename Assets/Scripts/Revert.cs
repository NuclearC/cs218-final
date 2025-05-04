using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Revert : MonoBehaviour
{
    [MenuItem("Test/Property Override")]
    static void TestPO()
    {
        Object[] meshRenderers = GameObject.FindObjectsOfType(typeof(MeshRenderer));

        foreach (MeshRenderer mr in meshRenderers)
        {

        }
    }
}
