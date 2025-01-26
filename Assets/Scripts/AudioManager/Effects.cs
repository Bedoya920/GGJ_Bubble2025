using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AudioEffects
{
    public string name;
    public AudioClip audioFile;

    [Range(0, 1f)]
    public float volumeEffect;
    [Range(0.1f, 2f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
