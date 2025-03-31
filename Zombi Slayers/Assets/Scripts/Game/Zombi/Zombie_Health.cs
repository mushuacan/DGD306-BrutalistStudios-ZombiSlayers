using UnityEngine;
using DG.Tweening;

public class Zombie_Health : MonoBehaviour
{
    private float health;
    [SerializeField] private float leftCameraEdge;
    [SerializeField] private ZombiAtTheBack_Manager zombiATBM;
    [SerializeField] private ZombiCharacter zombiChar;
    [SerializeField] private LaneFinder laner;


    private void OnEnable()
    {
        health = zombiChar.zombi.health;
    }

    private void Start()
    {
        if (zombiATBM == null) 
            Debug.LogError("zombiATBM tanýmlý deðil -> " + this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Bullet"))
        {
            Debug.Log(health);
            health -= collision.GetComponent<PlayerBullet>().damage;
            Debug.Log("After: " + health);
            if (health <= 0)
            {
                DOTween.Kill(transform);
                Destroy(this.gameObject);
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
