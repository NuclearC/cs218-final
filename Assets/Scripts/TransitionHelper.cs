using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionHelper : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject[] hideObjects;

    [SerializeField] AudioSource transitionSound;
    float startTime = 0;

    int transitionTo = -1;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTo >= 0)
        {
            if (fadeImage.gameObject.activeSelf == false)
            {
                startTime = Time.time;
                fadeImage.gameObject.SetActive(true);
                fadeImage.color = new Color(0, 0, 0, 0);

                transitionSound.Play();
            }
            else
            {
                float elapsed = Time.time - startTime;
                if (elapsed < 2.0f)
                {
                    float alpha = elapsed / 2.0f;
                    fadeImage.color = new Color(0, 0, 0, alpha);
                }
                else
                {
                    SceneManager.LoadScene(transitionTo);
                }
            }
        }
        else
        {
            if (fadeImage.gameObject.activeSelf == false) return;

            float elapsed = Time.time - startTime;

            if (hideObjects.Count() > 0)
            {
                elapsed -= 3.0f;
                if (elapsed >= 0 && hideObjects[0].activeSelf)
                {
                    foreach (var go in hideObjects)
                    {
                        go.SetActive(false);
                    }
                }
            }

            if (elapsed >= 0.0f)
            {
                if (elapsed < 2.0f)
                {
                    float alpha = 1 - elapsed / 2.0f;
                    fadeImage.color = new Color(0, 0, 0, alpha);
                }
                else fadeImage.gameObject.SetActive(false);
            }
        }
    }

    public void TransitionTo(int scene)
    {
        transitionTo = scene;
    }
}
