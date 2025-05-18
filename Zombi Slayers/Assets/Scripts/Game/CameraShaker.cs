using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float duration = 0.3f;
    public float strength = 0.3f;
    public int vibrato = 20;
    public float randomness = 90f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Shake()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true)
                 .OnKill(() => ReturnToInitialPosition());
    }

    void ReturnToInitialPosition()
    {
        transform.DOMove(initialPosition, 0.01f);
    }
}