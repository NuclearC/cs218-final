
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] string[] hintStrings;
    [SerializeField] string[] hazardStrings;

    [SerializeField] string[] timedHints;
    [SerializeField] float[] timedHintTimes;

    bool[] passedHints;
    bool[] passedTimedHints;
    bool[] passedHazards;
    private float startTime = 0;


    void Awake()
    {
        passedHints = new bool[hintStrings.Length];
        passedHazards = new bool[hazardStrings.Length];
        passedTimedHints = new bool[timedHintTimes.Length];
    }

    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {

        if (timedHintTimes.Length == timedHints.Length)
        {
            for (int i = 0; i < timedHintTimes.Length; i++)
            {
                if (timedHintTimes[i] <= (Time.time - startTime) && passedTimedHints[i] == false)
                {
                    passedTimedHints[i] = true;
                    UIManager.Instance.ShowTopPanel(timedHints[i]);
                }
            }
        }
    }
    public void ShowHint(int index)
    {
        if (index >= passedHints.Length || passedHints[index])
            return;
        passedHints[index] = true;
        UIManager.Instance.ShowTopPanel(hintStrings[index]);
    }

    public void ShowHazard(int index)
    {
        if (index >= passedHazards.Length || passedHazards[index])
            return;
        passedHazards[index] = true;
        UIManager.Instance.ShowBottomPanel(hazardStrings[index]);
    }
}

