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
    [SerializeField] private SpriteRenderer[] spriteRenderer;
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
            //Debug.LogError("zombiATBM tan�ml� de�il -> " + this.gameObject);
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
                Debug.LogWarning("�arpan obje 'Player_Bullet' tag'ine sahip ama PlayerBullet scripti yok." + collision.name);
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
                KillZombi();
            }
            else
            {
                if (bullet.weaponName == "Shotgun")
                {
                    if (damaj < 24)
                    {
                        Destroy(collision.gameObject);
                    }
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
    private void KillZombi()
    {
        ScoreManager.Instance.AddScore(killScore, "Zombi");

        // E�er explosioner varsa, patlama animasyonunu ba�lat
        if (explosioner != null)
        {
            explosioner.Explode();
        }

        // DOTween animasyonlar�n� iptal et
        DOTween.Kill(transform);
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            DOTween.Kill(spriteRenderer[i]);
        }
        DOTween.Kill(this.transform);
        DOTween.Kill(GetComponentInChildren<Transform>());

        // Obje yok edilmeden �nce animasyonlar� iptal et
        Destroy(this.gameObject);
    }

    public void DamageZombi(int damaj)
    {
        health -= damaj;
    }
}
