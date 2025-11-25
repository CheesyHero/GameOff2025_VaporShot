using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Mixer Reference")]
    public AudioMixer mixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        LoadVolume("Master", masterSlider);
        LoadVolume("Music", musicSlider);
        LoadVolume("Sounds", sfxSlider);

        masterSlider.onValueChanged.AddListener(v => SetVolume("Master", v));
        musicSlider.onValueChanged.AddListener(v => SetVolume("Music", v));
        sfxSlider.onValueChanged.AddListener(v => SetVolume("Sounds", v));
    }
    void SetVolume(string exposedParam, float linear)
    {
        float dB = Mathf.Log10(Mathf.Max(linear, 0.0001f)) * 20f;
        mixer.SetFloat(exposedParam, dB);
    }

    void LoadVolume(string exposedParam, Slider slider)
    {
        if (mixer == null || slider == null)
            return;

        if (mixer.GetFloat(exposedParam, out float dB)) 
            slider.SetValueWithoutNotify(DecibelToLinear(dB)); 
    } 
    public static float DecibelToLinear(float dB)
    {
        return Mathf.Pow(10f, dB / 20f);
    }
    public static float LinearToDecibel(float linear)
    {
        if (linear <= 0.0001f) return -80f; 
        return Mathf.Log10(linear) * 20f;
    }
}
