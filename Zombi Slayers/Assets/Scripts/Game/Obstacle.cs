using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public bool isBreakable;
    public bool isSlideable;

    [SerializeField] private AudioClip[] clipsForPlayerDamaj;
    [SerializeField] private AudioClip[] clipsForObstacleDestr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSlideable && collision.GetComponent<Player_Movement>().action == Player_Movement.ActionOC.SecondAbility && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
            { // eðer oyuncu slide yapýyorsa ve bu engel slideable ise hasar verme
                return;
            }
            if (All_Sounder.Instance != null && clipsForPlayerDamaj != null && clipsForPlayerDamaj.Length != 0 && collision.GetComponent<Player_Health>().health > 1)
            {
                All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForPlayerDamaj);
            }
            collision.GetComponent<Player_Health>().GiveDamage(!isSlideable);
        }

        if (isBreakable && collision.CompareTag("Player_Bullet"))
        {
            if (collision.GetComponent<PlayerBullet>().weaponName == "Sledgehammer")
            {
                if (All_Sounder.Instance != null && clipsForObstacleDestr != null && clipsForObstacleDestr.Length != 0)
                {
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForObstacleDestr);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
