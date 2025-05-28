using DG.Tweening;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    private Sequence flashSequence;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Health playerHealth = collision.GetComponent<Player_Health>();
            if (playerHealth != null )
            {
                if (playerHealth.health < 4 && playerHealth.health != 0)
                {
                    playerHealth.TakeMedKit();
                    collider2d.enabled = false;

                    Destroy(gameObject); //FlashAndDestroy(Color.green);
                }
            }
        }
    }
    // Bu metot chat gpt kullanýlarak yapýlmýþtýr.
    private void FlashAndDestroy(Color color)
    {
        if (flashSequence != null && flashSequence.IsActive())
        {
            flashSequence.Kill();
        }

        flashSequence = DOTween.Sequence();

        Color originalColor = Color.white;
        Color flashColor = color;

        for (int i = 0; i < 2; i++)
        {
            flashSequence.Append(spriteRenderer.DOColor(flashColor, 0.8f / 4));
            flashSequence.Append(spriteRenderer.DOColor(originalColor, 0.8f / 4));
        }

        flashSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
