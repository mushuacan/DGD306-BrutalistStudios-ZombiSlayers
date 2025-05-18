using DG.Tweening;
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
        if (collision.CompareTag("Zombi_Bullet"))
        {
            Destroy(collision.gameObject);
            return;
        }
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Zombi") || collision.CompareTag("Box") || collision.CompareTag("Supply"))
        {
            if (meltOnCollision)
            {
                SpriteRenderer spriteRenderer = collision.GetComponentInChildren<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // Önce zombinin collision'ýný yok ediyoruz, böylece mermi içinden geçer.
                    Collider2D zombiCollider = collision.GetComponent<Collider2D>();
                    if (zombiCollider != null)
                    {
                        zombiCollider.enabled = false;  // Zombinin çarpýþma alanýný kapatýyoruz.
                    }

                    Sequence seq = DOTween.Sequence();
                    // Objeyi yeþile döndürme ve küçültme
                    seq.Append(spriteRenderer.DOColor(Color.green, 0.5f).SetTarget(spriteRenderer));
                    seq.Append(collision.transform.DOScale(new Vector3(1.4f, 0, 1f), 1f).SetTarget(collision.transform));
                    seq.Join(collision.transform.DOMoveY(transform.position.y - 1, 1f).SetTarget(collision.transform));

                    seq.OnComplete(() =>
                    {
                        if (spriteRenderer != null)
                        {
                            DOTween.Kill(collision.transform);
                        }
                        // Yalnýzca animasyon tamamlandýysa objeyi yok et
                        Destroy(collision.gameObject);
                    });
                }

            }
            else if (hitOnCollision)
            {
                DOTween.Kill(collision.transform);
                if (collision.CompareTag("Zombi"))
                {
                    collision.GetComponent<Zombie_Health>().DamageZombi(15);
                }
                if (collision.CompareTag("Supply"))
                {
                    collision.DOKill(collision.transform);
                    Destroy(collision.gameObject);
                    return;
                }

                Sequence jumpSeq = DOTween.Sequence();

                // X yönüne git
                collision.transform.DOMoveX(collision.transform.position.x + 2, 1f);

                // Y yönüne önce yukarý sonra aþaðý
                jumpSeq.Append(collision.transform.DOMoveY(collision.transform.position.y + 1f, 0.5f).SetEase(Ease.OutQuad));
                jumpSeq.Append(collision.transform.DOMoveY(LaneFinder.laneYPositions[collision.GetComponent<LaneFinder>().lane], 0.5f).SetEase(Ease.InQuad));

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
