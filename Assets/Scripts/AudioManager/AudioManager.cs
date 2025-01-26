using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource; // AudioSource para la música
    public Slider musicVolumeSlider; // Slider para controlar el volumen de la música

    void Start()
    {
        // Configura el valor inicial del slider
        musicVolumeSlider.value = musicSource.volume;

        // Asigna el método que se llamará cuando el valor del slider cambie
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float volume)
    {
        musicSource.volume = volume; // Ajusta el volumen del AudioSource de la música
    }
}
