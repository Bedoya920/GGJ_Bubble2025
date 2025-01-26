using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSFXSettings : MonoBehaviour
{

    public AudioMixer myMixerSFX;
    public Slider sfxSlider;

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixerSFX.SetFloat("SFX", volume);
    }
}
