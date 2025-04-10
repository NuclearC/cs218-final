using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeBehavior : MonoBehaviour
{
    [SerializeField] float explosionTimeDelay = 5.0f;
    [SerializeField] GameObject[] explosionParticles;
    [SerializeField] GameObject objectToHide;
    private float explosionTime = 0.0f;
    private bool exploded = false;
    void Start()
    {
        explosionTime = Time.time + explosionTimeDelay;
    }

    private void Explode()
    {
        objectToHide.SetActive(false);
        foreach (var go in explosionParticles)
        {
            Instantiate(go, objectToHide.transform.position, Quaternion.identity, transform);
        }
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
