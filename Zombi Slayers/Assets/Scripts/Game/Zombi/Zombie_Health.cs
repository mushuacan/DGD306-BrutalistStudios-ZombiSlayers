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
    [SerializeField] private Zombi_Push pusher;
    [SerializeField] private AudioPlayer audioPlayer;


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
            health -= collision.GetComponent<PlayerBullet>().damage;
            if (health <= 0)
            {
                DOTween.Kill(transform);
                ScoreManager.Instance.AddScore(killScore, "Zombi");
                audioPlayer.PlaySound();
                Destroy(this.gameObject);
            }
            else
            {
                if (pusher != null)
                    pusher.PushBack(true, true);
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
