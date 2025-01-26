using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip music;
    public AudioClip[] audioBubbleClips;

    public static AudioManager instance;
    private int randomIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }
    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayRandomAudio()
    {
        if (audioBubbleClips.Length > 0)
        {
            
            randomIndex = Random.Range(0, audioBubbleClips.Length);

            
            SFXSource.clip = audioBubbleClips[randomIndex];

           
            SFXSource.Play();
        }
        else
        {
            Debug.LogWarning("No hay AudioClips en la lista.");
        }
    }
}
