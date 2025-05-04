

using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayButton : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    public void ChangeScene()
    {
        backgroundImage.color = new Color(0, 0, 0.5f);

        var transition = FindObjectOfType<TransitionHelper>();
        transition.TransitionTo(2);
    }

}
