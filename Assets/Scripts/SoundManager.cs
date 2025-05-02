using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManager = null;

    [SerializeField] AudioClip rifleFireAudio;

    [SerializeField] AudioClip[] shellSounds;
    [SerializeField] AudioClip[] impactSounds;
    [SerializeField] AudioClip[] fleshImpactSounds;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip equipSound;
    public static SoundManager GetSoundManager()
    {
        return soundManager == null ? (soundManager = FindObjectOfType<SoundManager>()) : soundManager;
    }


    public void OnRifleFire(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(rifleFireAudio, point, 0.3f);
    }

    public void PlayShellDropSound(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(shellSounds[Random.Range(0, shellSounds.Count())], point, 0.5f);
    }
    public void PlayImpactSound(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(impactSounds[Random.Range(0, impactSounds.Count())], point);
    }
    public void PlayFleshImpactSound(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(fleshImpactSounds[Random.Range(0, fleshImpactSounds.Count())], point);
    }

    public void PlayReloadSound(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(reloadSound, point, 1.0f);
    }
    public void PlayEquipSound(Vector3 point)
    {
        AudioSource.PlayClipAtPoint(equipSound, point, 1.0f);
    }
}
