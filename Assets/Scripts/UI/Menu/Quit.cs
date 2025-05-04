using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops play mode in editor
#else
        Application.Quit(); // Quits built game
#endif
    }
}
