using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_BOSS_Bullets : MonoBehaviour
{
    [SerializeField] private bool isItTurning;
    [SerializeField] private bool meltOnCollision;
    [SerializeField] private bool hitOnCollision;
    [SerializeField] private float moveDistance = 15f;
    [SerializeField] private float moveDuration = 2f;

    private Tween rotationTween; // D�nd�rme tween referans�

    private void Start()
    {
        MoveObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Zombi") || collision.CompareTag("Box") || collision.CompareTag("Supply") || collision.CompareTag("Zombi_Bullet"))
        {
            if (meltOnCollision)
            {
                // SpriteRenderer'� hem kendisinde hem de child'lar�nda ara
                SpriteRenderer spriteRenderer = collision.GetComponentInChildren<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // DOTween Sequence: Animasyonlar� s�rayla �al��t�r
                    Sequence seq = DOTween.Sequence();

                    // 1. Ye�ile d�n��
                    seq.Append(spriteRenderer.DOColor(Color.green, 1f));

                    // 2. Eritme efekti: �effaf + K���lme
                    seq.Append(spriteRenderer.DOFade(0f, 0.5f)); // �effafla�ma

                    seq.Join(collision.transform.DOScale(Vector3.zero, 0.5f)); // K���lme

                    // 3. Obje yok edilsin
                    seq.OnComplete(() =>
                    {
                        DOTween.Kill(collision.transform); Destroy(collision.gameObject);
                    }
                );
                }
            }
            else if (hitOnCollision)
            {
                if (collision.CompareTag("Zombi_Bullet"))
                {
                    Destroy(collision.gameObject);
                }

                Sequence jumpSeq = DOTween.Sequence();

                // X y�n�ne git
                collision.transform.DOMoveX(collision.transform.position.x + 2, 1f);

                // Y y�n�ne �nce yukar� sonra a�a��
                jumpSeq.Append(collision.transform.DOMoveY(collision.transform.position.y + 1f, 0.5f).SetEase(Ease.OutQuad));
                jumpSeq.Append(collision.transform.DOMoveY(collision.transform.position.y, 0.5f).SetEase(Ease.InQuad));

            }
        }
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
