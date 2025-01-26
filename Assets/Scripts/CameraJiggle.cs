using UnityEngine;
using DG.Tweening;

public class CameraJiggle : MonoBehaviour
{
    public void Jiggle(float duration, float strength, int vibrato)
    {
        // Aplica una sacudida (shake) a la cámara
        transform.DOShakePosition(duration, strength, vibrato);
    }
}
