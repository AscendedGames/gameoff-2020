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
        masterVolumeSlider.value = PlayerPrefs.GetFloat("VolumePreference", 0.25f);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}