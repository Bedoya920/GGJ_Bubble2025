using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource; // AudioSource para la m�sica
    public Slider musicVolumeSlider; // Slider para controlar el volumen de la m�sica

    void Start()
    {
        // Configura el valor inicial del slider
        musicVolumeSlider.value = musicSource.volume;

        // Asigna el m�todo que se llamar� cuando el valor del slider cambie
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float volume)
    {
        musicSource.volume = volume; // Ajusta el volumen del AudioSource de la m�sica
    }
}
