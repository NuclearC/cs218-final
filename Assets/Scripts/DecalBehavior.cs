using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalBehavior : MonoBehaviour
{
    private DecalProjector decalProjector;
    private float beginTime;

    [SerializeField] float fadeOutAfter = 10.0f;
    [SerializeField] float fadeOutTime = 5.0f;

    void Start()
    {
        beginTime = Time.time;
        decalProjector = GetComponent<DecalProjector>();

        if (!decalProjector)
            Destroy(gameObject, fadeOutAfter);
    }

    void Update()
    {
        if (!decalProjector)
            return;
        if (Time.time >= fadeOutAfter + beginTime)
        {
            if (Time.time >= fadeOutAfter + fadeOutTime + beginTime)
                Destroy(gameObject);
            else
            {
                float opacity = (Time.time - (fadeOutAfter + beginTime)) / fadeOutTime;
                decalProjector.fadeFactor = 1.0f - opacity;
            }
        }
    }
}
