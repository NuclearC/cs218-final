using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour
{
    private float lastDamageTime = 0.0f;
    [SerializeField] float damageInterval = 0.5f;
    [SerializeField] int damage = 5;
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<HealthManager>(out var healthManager))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                lastDamageTime = Time.time;
                healthManager.AddHealth(-damage);
            }
        }
    }
}
