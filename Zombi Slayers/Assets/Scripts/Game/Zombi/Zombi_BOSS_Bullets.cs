using DG.Tweening;
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
                    // �nce zombinin collision'�n� yok ediyoruz, b�ylece mermi i�inden ge�er.
                    Collider2D zombiCollider = collision.GetComponent<Collider2D>();
                    if (zombiCollider != null)
                    {
                        zombiCollider.enabled = false;  // Zombinin �arp��ma alan�n� kapat�yoruz.
                    }
                    spriteRenderer.DOColor(Color.green, 0.5f).SetTarget(spriteRenderer).SetLink(collision.gameObject);
                }

                // �lk animasyon tamamland���nda di�erlerini ba�lat
                if (collision != null && collision.transform != null)
                {
                    // �l�ek k���ltme
                    collision.transform.DOScale(new Vector3(1.4f, 0f, 1f), 1f).SetTarget(collision.transform).SetLink(collision.gameObject);

                    collision.transform.DOMoveY(transform.position.y - 1, 1f).SetTarget(collision.transform).SetLink(collision.gameObject).OnComplete(() =>
                    {
                        if (collision != null)
                        {
                            DOTween.Kill(collision.transform);
                            Destroy(collision.gameObject);
                        }
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
                }// X y�n�nde ileri git (paralel olarak)
                if (collision != null && collision.transform != null)
                {
                    collision.transform.DOMoveX(collision.transform.position.x + 2, 1f)
                        .SetLink(collision.gameObject);
                }

                if (collision != null && collision.transform != null)
                {
                    collision.transform.DOMoveY(collision.transform.position.y + 1f, 0.5f).SetEase(Ease.OutQuad).SetLink(collision.gameObject).OnComplete(() =>
                        {
                            if (collision != null && collision.transform != null)
                            {
                                float targetY = LaneFinder.laneYPositions[collision.GetComponent<LaneFinder>().lane];

                                collision.transform.DOMoveY(targetY, 0.5f).SetEase(Ease.InQuad).SetLink(collision.gameObject);
                            }
                        });
                }


            }
        }
    }

    private void MoveObject()
    {
        transform.DOMoveX(transform.position.x + moveDistance, moveDuration)
                 .SetEase(Ease.Linear).SetLink(gameObject)
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
                .SetLoops(-1, LoopType.Restart).SetLink(gameObject); // Sonsuz d�ng�
        }
    }
}
