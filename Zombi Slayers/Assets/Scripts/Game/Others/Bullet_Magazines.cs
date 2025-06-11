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
                    Destroy(gameObject); 
                }
                float easyness = (float)GameSettings.Instance.settings["difficulty"];
                if (magazineType == MagazineTypes.Shotgun && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
                {
                    if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                    if (easyness == 0) magazineBulletCount += 2;
                    if (easyness == 0.5f) magazineBulletCount += 1;
                    player_Attack.TakeMagazine(magazineBulletCount);
                    collider2d.enabled = false;
                    Destroy(gameObject); 
                }
                if (magazineType == MagazineTypes.Sniper && collision.GetComponent<Player_Character>().character.characterName == "Derrick")
                {
                    if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                    if (easyness == 0) magazineBulletCount += 2;
                    if (easyness == 0.5f) magazineBulletCount += 1;
                    player_Attack.TakeMagazine(magazineBulletCount);
                    collider2d.enabled = false;
                    Destroy(gameObject); 
                }
            }
        }
    }
}
