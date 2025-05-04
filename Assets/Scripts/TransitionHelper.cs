using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHelper : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject[] hideObjects;
    float startTime = 0;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeImage.gameObject.activeSelf == false) return;
        float elapsed = Time.time - startTime;
        if (elapsed < 2.0f)
        {
            float alpha = 1 - elapsed / 2.0f;
            fadeImage.color = new Color(0, 0, 0, alpha);
        }
        else fadeImage.gameObject.SetActive(false);
    }
}
