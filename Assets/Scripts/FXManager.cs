using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    [SerializeField] GameObject bulletImpactParticles;
    [SerializeField] GameObject bloodParticles;
    [SerializeField] GameObject impactSmokeParticles;


    private static FXManager fXManager = null;
    public static FXManager GetEffectsManager()
    {
        return fXManager == null ? (fXManager = FindObjectOfType<FXManager>()) : fXManager;
    }

    public void OnImpactSmoke(GameObject go, Vector3 hitPoint, Vector3 hitNormal)
    {
        Instantiate(impactSmokeParticles, hitPoint, Quaternion.LookRotation(hitNormal), go.transform);
    }

    public void OnImpact(GameObject go, Vector3 hitPoint, Vector3 direction)
    {
        Instantiate(bulletImpactParticles, hitPoint, Quaternion.LookRotation(direction), go.transform);
    }
    public void OnBloodImpact(GameObject go, Vector3 hitPoint, Vector3 direction)
    {
        // Debug.Log("wtfwtfwtf");
        Instantiate(bloodParticles, hitPoint, Quaternion.LookRotation(direction), go.transform);
    }
}
