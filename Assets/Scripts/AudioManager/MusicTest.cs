using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.bubble);
        }
    }
}
