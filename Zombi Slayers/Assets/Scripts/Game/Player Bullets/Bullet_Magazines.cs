using DG.Tweening;
using UnityEngine;

public class Bullet_Magazines : MonoBehaviour
{
    [SerializeField] private int magazineBulletCount;
    [SerializeField] private MagazineTypes magazineType;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Collider2D collider2d;
    private enum MagazineTypes
    {
        Dynamite,
        Shotgun
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
                    transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
                }
                if (magazineType == MagazineTypes.Shotgun && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
                {
                    if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                    player_Attack.TakeMagazine(magazineBulletCount);
                    collider2d.enabled = false;
                    transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
