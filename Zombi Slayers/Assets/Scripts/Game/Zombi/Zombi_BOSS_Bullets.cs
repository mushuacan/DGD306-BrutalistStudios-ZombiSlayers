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

    private Tween rotationTween; // Döndürme tween referansý

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
                // SpriteRenderer'ý hem kendisinde hem de child'larýnda ara
                SpriteRenderer spriteRenderer = collision.GetComponentInChildren<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // DOTween Sequence: Animasyonlarý sýrayla çalýþtýr
                    Sequence seq = DOTween.Sequence();

                    // 1. Yeþile dönüþ
                    seq.Append(spriteRenderer.DOColor(Color.green, 1f));

                    // 2. Eritme efekti: Þeffaf + Küçülme
                    seq.Append(spriteRenderer.DOFade(0f, 0.5f)); // Þeffaflaþma

                    seq.Join(collision.transform.DOScale(Vector3.zero, 0.5f)); // Küçülme

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

                // X yönüne git
                collision.transform.DOMoveX(collision.transform.position.x + 2, 1f);

                // Y yönüne önce yukarý sonra aþaðý
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
            // Dönme animasyonu (kendi etrafýnda -Z yönünde)
            // Sonsuz dönüþ yapýyoruz ama obje yok olunca zaten duracak
            rotationTween = transform.DORotate(
                new Vector3(0f, 0f, -360f), // -Z yönünde tam tur
                1f,                         // Her 1 saniyede bir tam tur
                RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart); // Sonsuz döngü
        }
    }
}
