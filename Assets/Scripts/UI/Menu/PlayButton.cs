

using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayButton : MonoBehaviour
{
    [SerializeField] Image backgroundImage;

    private AudioSource stopMusic;

    void Start()
    {
        stopMusic = GameObject.FindWithTag("MusicSingleton").GetComponent<AudioSource>();
    }
    public void ChangeScene()
    {
        backgroundImage.color = new Color(0, 0, 0.5f);

        var transition = FindObjectOfType<TransitionHelper>();
        transition.TransitionTo(2);

        stopMusic.Stop();
        Destroy(stopMusic.gameObject);
    }

}
