using UnityEngine;
using DG.Tweening;

public class MoveAndBounce : MonoBehaviour
{
    public Transform target; // El punto B al que se moverá el sprite
    public float moveDuration = 1f; // Duración del movimiento
    public float bounceDistance = 1f; // Distancia del rebote (ajustado a 1)
    public float bounceDuration = 0.5f; // Duración del rebote

    void Start()
    {
        // Inicia el movimiento
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // Mueve el sprite al punto B
        transform.DOMove(target.position, moveDuration)
            .SetEase(Ease.Linear) // Movimiento lineal
            .OnComplete(() =>
            {
                // Aplica el efecto de rebote al llegar
                BounceEffect();
            });
    }

    void BounceEffect()
    {
        // Realiza el rebote
        transform.DOPunchPosition(new Vector3(0, bounceDistance, 0), bounceDuration, 1, 0)
            .OnComplete(() =>
            {
                // Aquí puedes agregar cualquier acción adicional después del rebote
                Debug.Log("Rebote completado");
            });
    }
}