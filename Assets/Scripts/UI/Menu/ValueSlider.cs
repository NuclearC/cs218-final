using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;  // ← Add this

public class ValueSlider : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;  // ← Change from Text to TMP_Text

    private AudioVolumeController volumeController;
    void Start()
    {
        volumeController = GetComponent<AudioVolumeController>();
        slider.value = volumeController.GetVolume();
        valueText.text = slider.value.ToString();

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        if (valueText != null)
        {
            int vol = Mathf.RoundToInt(value);
            volumeController.SetVolume(vol);
            valueText.text = vol.ToString();
        }
    }
}

