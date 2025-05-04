using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ← Add this

public class ValueSlider : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;  // ← Change from Text to TMP_Text

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        if (valueText != null)
            valueText.text = Mathf.RoundToInt(value).ToString();
    }
}

