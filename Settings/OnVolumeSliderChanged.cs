using UnityEngine;
using UnityEngine.Audio;

public class OnVolumeSliderChanged : MonoBehaviour
{
    public AudioMixer mixer;
    public string exposedVolumeParam = "MasterVolume";

    public void ChangeSlider(float value)
    {
        float dB = AudioSettingsUI.LinearToDecibel(value);
        mixer.SetFloat(exposedVolumeParam, dB);
    }

}
