using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    public int GetVolume()
    {
        return Mathf.RoundToInt(100.0f * AudioListener.volume);
    }
    public void SetVolume(int volume)
    {
        AudioListener.volume = (float)volume / 100.0f;
    }
}
