using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBehavior : MonoBehaviour
{
    [SerializeField][Range(0, 10)] float disappearAfterTime = 5.0f;
    [SerializeField] TMPro.TMP_Text captionText;

    float disappearTime = 0.0f;

    void Update()
    {
        if (disappearTime > 0 && Time.time >= disappearTime)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Show(string caption)
    {
        captionText.text = caption;
        gameObject.SetActive(true);

        disappearTime = Time.time + disappearAfterTime;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
