using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeBehavior : MonoBehaviour
{
    [SerializeField] float explosionTimeDelay = 5.0f;
    [SerializeField] GameObject[] explosionParticles;
    [SerializeField] GameObject objectToHide;

    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip debrisSound;

    [SerializeField] float range = 10.0f;
    [SerializeField] int baseDamage = 95;

    private float explosionTime = 0.0f;
    private bool exploded = false;
    void Start()
    {
        explosionTime = Time.time + explosionTimeDelay;
    }

    void DamageWave(Vector3 origin)
    {
        var colliders = Physics.OverlapSphere(origin, range);

        foreach (var col in colliders)
        {
            float distance = Vector3.Distance(col.transform.position, origin);
            if (distance > range) continue;
            float modifier = 1.0f - (float)Math.Sqrt(distance / range);

            print(col.tag);
            if (col.TryGetComponent<HealthManager>(out var healthManager))
            {
                healthManager.AddHealth(-(int)(baseDamage * modifier));
            }

            if (col.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                var pt = col.ClosestPoint(origin);
                rigidbody.AddForceAtPosition((pt - origin) * modifier * 2.0f, pt, ForceMode.Impulse);
            }
        }
    }

    private void Explode()
    {
        objectToHide.SetActive(false);
        var explodePosition = objectToHide.transform.position;
        foreach (var go in explosionParticles)
        {
            Instantiate(go, explodePosition, Quaternion.Euler(-90, 0, 0), transform);
        }
        DamageWave(explodePosition);
        AudioSource.PlayClipAtPoint(explosionSound, explodePosition);
        AudioSource.PlayClipAtPoint(debrisSound, explodePosition);
        Destroy(gameObject, 5.0f);
        exploded = true;
    }
    void Update()
    {
        if (Time.time >= explosionTime && !exploded)
        {
            Explode();
        }
    }
}
