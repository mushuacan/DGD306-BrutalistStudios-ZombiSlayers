using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_BOSS_Bullets : MonoBehaviour
{
    [SerializeField] private bool isItTurning;
    [SerializeField] private float moveDistance = 15f;
    [SerializeField] private float moveDuration = 2f;

    private Tween rotationTween; // D�nd�rme tween referans�

    private void Start()
    {
        MoveObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void MoveObject()
    {
        transform.DOMoveX(transform.position.x + moveDistance, moveDuration)
                 .SetEase(Ease.Linear)
                 .OnComplete(() =>
                 {
                     if (rotationTween != null && rotationTween.IsActive())
                     {
                         rotationTween.Kill();
                     }

                     // Sonra objeyi yok et
                     Destroy(gameObject);
                 });

        if (isItTurning)
        {
            // D�nme animasyonu (kendi etraf�nda -Z y�n�nde)
            // Sonsuz d�n�� yap�yoruz ama obje yok olunca zaten duracak
            rotationTween = transform.DORotate(
                new Vector3(0f, 0f, -360f), // -Z y�n�nde tam tur
                1f,                         // Her 1 saniyede bir tam tur
                RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart); // Sonsuz d�ng�
        }
    }
}
