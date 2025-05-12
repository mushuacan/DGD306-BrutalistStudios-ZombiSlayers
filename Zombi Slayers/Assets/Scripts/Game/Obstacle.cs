using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public bool isBreakable;
    public bool isSlideable;

    [SerializeField] private AudioClip[] audioClips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSlideable && collision.GetComponent<Player_Movement>().action == Player_Movement.ActionOC.SecondAbility )
            { // eðer oyuncu slide yapýyorsa ve bu engel slideable ise hasar verme
                return;
            }
            collision.GetComponent<Player_Health>().GiveDamage(!isSlideable);
        }

        if (isBreakable && collision.CompareTag("Player_Bullet"))
        {
            if (collision.GetComponent<PlayerBullet>().weaponName == "Sledgehammer")
            {
                if (All_Sounder.Instance != null && audioClips != null)
                {
                    All_Sounder.Instance.ChooseAndPlaySoundOf(audioClips);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
