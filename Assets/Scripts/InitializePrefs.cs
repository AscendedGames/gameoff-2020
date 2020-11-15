using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InitializePrefs : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public float InitialMasterVolume;

    // Start is called before the first frame update
    void Start()
    {
        InitializeVolume();
    }

    private void InitializeVolume()
    {
        if (!PlayerPrefs.HasKey("VolumePreference"))
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(0.25f) * 20);
            PlayerPrefs.SetFloat("VolumePreference", 0.25f);
        }
        else masterVolumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("VolumePreference", sliderValue);
    }
}