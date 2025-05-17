using UnityEngine;
using DG.Tweening;

public class Zombie_Health : MonoBehaviour
{
    private float health;
    [SerializeField] private float leftCameraEdge;
    [SerializeField] private int killScore;
    [SerializeField] private ZombiAtTheBack_Manager zombiATBM;
    [SerializeField] private ZombiCharacter zombiChar;
    [SerializeField] private LaneFinder laner;
    [SerializeField] private CreateExplosionWhileDying explosioner;
    [SerializeField] private Zombi_Push pusher;
    [SerializeField] private AudioClip[] clipsLowDamaj;
    [SerializeField] private AudioClip[] clipsHighDamaj;

    private void OnEnable()
    {
        health = zombiChar.zombi.health;
    }

    private void Start()
    {
        if (zombiATBM == null)
        {
            //Debug.LogError("zombiATBM tanýmlý deðil -> " + this.gameObject);
            zombiATBM = FindObjectOfType<ZombiAtTheBack_Manager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Bullet"))
        {

            PlayerBullet bullet = collision.GetComponent<PlayerBullet>();
            float damaj = 0;
            if (bullet != null)
            {
                damaj = bullet.damage;
            }
            else
            {
                Debug.LogWarning("Çarpan obje 'Player_Bullet' tag'ine sahip ama PlayerBullet scripti yok." + collision.name);
                return;
            }
            health -= damaj;
            if (All_Sounder.Instance != null)
            {
                if (damaj < 24)
                {
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsLowDamaj);
                }
                else
                {
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsHighDamaj);
                }
            }
            if (health <= 0)
            {
                DOTween.Kill(transform);
                ScoreManager.Instance.AddScore(killScore, "Zombi");
                if(explosioner != null)
                {
                    explosioner.Explode();
                }
                Destroy(this.gameObject);
            }
            else
            {
                if (damaj < 24)
                {
                    Destroy(collision.gameObject);
                }
                if (pusher != null)
                {
                    pusher.PushBack(true, true);
                }
            }
        }
    }

    private void Update()
    {
        if (transform.position.x < leftCameraEdge)
        {
            if (zombiATBM != null)
            {
                zombiATBM.AddBackZombi(laner.lane);
            }
            DOTween.Kill(transform);
            Destroy(this.gameObject);
        }
    }
}
