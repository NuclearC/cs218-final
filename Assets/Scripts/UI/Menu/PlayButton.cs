
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayButton : MonoBehaviour
{
    public void ChangeScene()
    {

        var transition = FindObjectOfType<TransitionHelper>();
        transition.TransitionTo(2);
    }

}
