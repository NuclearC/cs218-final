using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVisionManager : MonoBehaviour
{

    [SerializeField] GameObject nightVisionTexture;


    public void Show()
    {
        nightVisionTexture.SetActive(true);
    }

    public void Hide()
    {
        nightVisionTexture.SetActive(false);
    }

    public void Toggle()
    {
        if (nightVisionTexture.activeSelf)
            Hide();
        else Show();
    }

    // Update is called once per frame
    void Update()
    {
        // nightVisionTexture.GetComponent<RawImage>().material.SetFloat("NoiseMultiplier", MathF.Sin(Time.time));

    }
}
