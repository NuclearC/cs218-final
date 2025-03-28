using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVisionManager : MonoBehaviour
{

    [SerializeField] GameObject nightVisionGameObject;
    private Animator nightVisionAnimator;

    void Start()
    {
        nightVisionAnimator = nightVisionGameObject.GetComponent<Animator>();
    }

    public void Show()
    {
        nightVisionAnimator.SetBool("enabled", true);
    }

    public void Hide()
    {
        nightVisionAnimator.SetBool("enabled", false);
    }

    public void Toggle()
    {
        if (nightVisionAnimator.GetBool("enabled"))
            Hide();
        else Show();
    }

    // Update is called once per frame
    void Update()
    {
        // nightVisionTexture.GetComponent<RawImage>().material.SetFloat("NoiseMultiplier", MathF.Sin(Time.time));

    }
}
