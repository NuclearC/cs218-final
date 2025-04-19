using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManager = null;

    [SerializeField] AudioClip rifleFireAudio;
    public static SoundManager GetSoundManager()
    {
        return soundManager == null ? (soundManager = FindObjectOfType<SoundManager>()) : soundManager;
    }


    public void OnRifleFire(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(rifleFireAudio, point);
    }
}
