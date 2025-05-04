using UnityEngine;

public class Room3MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip room3Music;
    private MusicController musicController;

    private void Start()
    {
        musicController = FindObjectOfType<MusicController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || musicController == null)
            return;

        musicController.PlayRoom3Music(room3Music);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || musicController == null)
            return;

        // musicController.PlayMainMusic();
    }
}
