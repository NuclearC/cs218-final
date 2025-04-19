
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] string[] hintStrings;
    [SerializeField] string[] hazardStrings;

    bool[] passedHints;
    bool[] passedHazards;

    void Awake()
    {
        passedHints = new bool[hintStrings.Length];
        passedHazards = new bool[hazardStrings.Length];
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

