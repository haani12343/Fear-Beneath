using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider sensitivitySlider;
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        masterVolumeSlider.value = volume;
        sensitivitySlider.value = sensitivity;
        AudioListener.volume = volume;
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }
}