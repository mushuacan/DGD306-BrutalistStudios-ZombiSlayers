using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float duration = 0.3f;
    public float strength = 0.3f;
    public int vibrato = 20;
    public float randomness = 90f;

    public void Shake()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
    }
}
