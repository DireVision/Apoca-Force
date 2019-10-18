using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* 
 * To use this script, just attach 2 sliders to it with a valid mixer separating BGM & SFX 
 */

public class VolumeControl : MonoBehaviour
{
    /// <summary>
    /// Cache a reference to the master mixer
    /// </summary>
    public AudioMixer mixer;

    public Slider BGMSlider;
    public Slider SFXSlider;

    void Start()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVol", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);
    }

    public void SetBGMLevel (float sliderValue)
    {
        mixer.SetFloat("BGMVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMVol", sliderValue);
    }

    public void SetSFXLevel (float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
    }
}
