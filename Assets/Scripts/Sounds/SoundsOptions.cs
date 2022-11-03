using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsOptions : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Slider masterSlider, soundtrackSlider, effectsSlider;

    void Start()
    {
        float currentVolume;
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        masterSlider.value = currentVolume;
        soundtrackSlider.value = currentVolume;
        effectsSlider.value = currentVolume;
    }

    public void MasterControl()
    {
        audioMixer.SetFloat("MasterVolume", masterSlider.value);
    }
    public void SoundtrackControl()
    {
        audioMixer.SetFloat("SoundtrackVolume", soundtrackSlider.value);
    }
    public void EffectsControl()
    {
        audioMixer.SetFloat("EffectsVolume", effectsSlider.value);
    }
}
