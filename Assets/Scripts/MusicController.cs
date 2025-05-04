using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip mainMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainMusic = audioSource.clip;  // Store the initial music as default
    }

    public void PlayRoom3Music(AudioClip newMusic)
    {
        if (audioSource.clip != newMusic)
        {
            audioSource.Stop();
            audioSource.pitch = 1.0f;
            audioSource.clip = newMusic;
            audioSource.Play();
        }
    }
}
