using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManager = null;

    [SerializeField] AudioSource rifleFireAudio;
    public static SoundManager GetSoundManager()
    {
        return soundManager == null ? (soundManager = FindObjectOfType<SoundManager>()) : soundManager;
    }


    public void OnRifleFire()
    {
        rifleFireAudio.Play();
    }
}
