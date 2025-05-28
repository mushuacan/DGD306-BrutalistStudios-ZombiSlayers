using DG.Tweening;
using UnityEngine;

public class Bullet_Magazines : MonoBehaviour
{
    [SerializeField] private int magazineBulletCount;
    [SerializeField] private MagazineTypes magazineType;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Sequence flashSequence;
    private enum MagazineTypes
    {
        Dynamite,
        Shotgun,
        Sniper
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Attack player_Attack = collision.GetComponent<Player_Attack>();
            if (player_Attack != null)
            {
                if (magazineType == MagazineTypes.Dynamite && collision.GetComponent<Player_Character>().character.characterName == "Woods")
                {
                    player_Attack.TakeDynamite();
                    collider2d.enabled = false;
                    Destroy(gameObject); // FlashAndDestroy(Color.green);
                }
                if (magazineType == MagazineTypes.Shotgun && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
                {
                    if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                    player_Attack.TakeMagazine(magazineBulletCount);
                    collider2d.enabled = false;
                    Destroy(gameObject); // FlashAndDestroy(Color.yellow);
                }
                if (magazineType == MagazineTypes.Sniper && collision.GetComponent<Player_Character>().character.characterName == "Derrick")
                {
                    if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                    player_Attack.TakeMagazine(magazineBulletCount);
                    collider2d.enabled = false;
                    Destroy(gameObject); // FlashAndDestroy(Color.green);
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
