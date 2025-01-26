using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioEffects[] audios;
    public static EffectManager ins;

    float volumeEffect, lastVolumeValue, volSaved, volumeEffectValue;
    private void Awake()
    {

        volSaved = PlayerPrefs.GetFloat("EffectVolume");

        foreach (AudioEffects a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.audioFile;
            a.source.volume = a.volumeEffect;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    public void Start()
    {
        volumeSlider.value = volSaved;
        volumeEffectValue = volumeSlider.value;
        ChangeVolume();
        //FindObjectOfType<AudioManager>().Play("Explotion1");    Para llamar un audio desde cualquier lado
    }

    private void Update()
    {
        volumeEffectValue = volumeSlider.value;
        if(volumeEffectValue != lastVolumeValue)
        {
            ChangeVolume();
            SaveVol();
        }

        lastVolumeValue = volumeEffectValue;
    }

    public void Play(string name)
    {
        AudioEffects a = Array.Find(audios, audioEffects => audioEffects.name == name);

        if(a == null)
        {
            Debug.LogWarning("Nulo");
            return;
        }
        a.source.Play();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeEffectValue;
    }

    public void SaveVol()
    {
        PlayerPrefs.SetFloat("EffectVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}
