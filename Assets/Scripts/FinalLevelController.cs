using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalLevelController : MonoBehaviour
{
    [SerializeField] GameObject objectToRemove;
    [SerializeField] AudioSource soundStart;
    [SerializeField] AudioSource soundEnd;
    [SerializeField] AudioSource soundEnd2;
    [SerializeField] Image fadeOutImage;
    private float timeCompleted = 0.0f;
    bool startCount = false;
    void Start()
    {
        EventManager.Instance.OnObjectiveCompleted.AddListener(OnObjectiveCompleted);
    }

    private void OnObjectiveCompleted()
    {
        timeCompleted = Time.time;
        startCount = true;
        soundStart.Play();
    }

    void Update()
    {
        if (!startCount)
            return;
        if (Time.time - timeCompleted > 15.0f)
        {
            print("Game finished");
            GameManager.Instance.BackToMenu();
        }
        else if (Time.time - timeCompleted > 12.0f)
        {
            float elapsed = Time.time - timeCompleted - 12.0f;
            float val = 1.0f - elapsed / 4.0f;
            fadeOutImage.color = new Color(val, val, val, 1.0f);
        }
        else if (Time.time - timeCompleted > 8.0f)
        {
            float elapsed = Time.time - timeCompleted - 8.0f;
            fadeOutImage.color = new Color(1, 1, 1, elapsed / 4.0f);
        }
        else if (objectToRemove && Time.time - timeCompleted > 5.0f)
        {
            soundEnd.Play();
            soundEnd2.Play();

            // HACK
            PlayerManager.GetLocalPlayerManager().Movement.Freeze = true;
            Destroy(objectToRemove);

            fadeOutImage.color = new Color(1, 1, 1, 0.0F);
            fadeOutImage.gameObject.SetActive(true);
        }
    }
}
