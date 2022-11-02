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
    Slider soundtrackSlider;

    void Start()
    {
        float currentVolume;
        audioMixer.GetFloat("SoundtrackVolume", out currentVolume);
        soundtrackSlider.value = currentVolume;
    }

    public void SoundtrackControl()
    {
        audioMixer.SetFloat("SoundtrackVolume", soundtrackSlider.value);
    }
}
