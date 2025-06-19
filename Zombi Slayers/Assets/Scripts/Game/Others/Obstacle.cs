using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public bool isBreakable;
    public bool isSlideable;

    public GameObject remaining;
    public GameObject obstacleParticles;

    [SerializeField] private AudioClip[] clipsForPlayerDamaj;
    [SerializeField] private AudioClip[] clipsForObstacleDestr;

    private void CreateRemaining()
    {
        Instantiate(remaining, transform.position, Quaternion.identity, transform.parent);
    }

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
                All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForPlayerDamaj, "Player Took Damaj", false);
            }
            collision.GetComponent<Player_Health>().GiveDamage(!isSlideable);
        }

        if (isBreakable && collision.CompareTag("Player_Bullet"))
        {
            if (collision.GetComponent<PlayerBullet>().weaponName == "Sledgehammer")
            {
                if (All_Sounder.Instance != null && clipsForObstacleDestr != null && clipsForObstacleDestr.Length != 0)
                {
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForObstacleDestr, "Obstacle Destroyed", false);
                }
                if (obstacleParticles != null)
                Instantiate(obstacleParticles, transform.position, Quaternion.identity);
                CreateRemaining();
                Destroy(this.gameObject);
            }
        }
        if (!isBreakable && collision.CompareTag("Player_Bullet"))
        {
            if (collision.GetComponent<PlayerBullet>().weaponName == "Derrick Kick")
            {
                transform.DOMoveX(transform.position.x + 2f, 0.5f).SetEase(Ease.Linear).SetLink(gameObject);

                transform.DOMoveY(transform.position.y + 0.5f, 0.25f).SetEase(Ease.OutQuad).SetLink(gameObject).OnComplete(() =>
                {
                    if (transform != null)
                    {
                        float targetY = LaneFinder.laneYPositions[GetComponent<LaneFinder>().lane];

                        transform.DOMoveY(targetY, 0.25f).SetEase(Ease.InQuad).SetLink(gameObject);
                    }
                });
            }
        }
    }
}
