using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalManager : MonoBehaviour
{

    [SerializeField] GameObject decal;
    [SerializeField] GameObject bloodDecal;
    private static DecalManager decalManager = null;
    public static DecalManager GetDecalManager()
    {
        return decalManager == null ? (decalManager = FindObjectOfType<DecalManager>()) : decalManager;
    }

    public void CreateBloodDecal(GameObject parent, Vector3 position, Vector3 direction)
    {
        Instantiate(bloodDecal, position, Quaternion.FromToRotation(Vector3.forward, direction), parent.transform);
    }
    public void CreateDecal(GameObject parent, Vector3 position, Vector3 direction)
    {
        Instantiate(decal, position, Quaternion.FromToRotation(Vector3.forward, direction), parent.transform);
    }
}
