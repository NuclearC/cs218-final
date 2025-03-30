using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    private static ParticlesManager particlesManager = null;
    public static ParticlesManager GetParticlesManager()
    {
        return particlesManager == null ? (particlesManager = FindObjectOfType<ParticlesManager>()) : particlesManager;
    }

    private ParticleSystem pSystem;
    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    public void Emit(Vector3 position, Vector3 normal)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        for (int i = 0; i < 6; i++)
        {
            emitParams.position = position;

            float speed = 5f;
            float rr = 90.0f;
            emitParams.velocity = Quaternion.Euler(Random.Range(-rr, rr), Random.Range(-rr, rr), 0.0f) * normal * speed;

            pSystem.Emit(emitParams, 1);
        }
    }
}
