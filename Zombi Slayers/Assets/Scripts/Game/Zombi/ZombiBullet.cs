using UnityEngine;
using DG.Tweening;

public class ZombiBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float leftCameraEdge;
    [SerializeField] private GameObject iadeiBullet;
    private bool move;

    public void Arrangements(float speed, float damage)
    {
        this.speed = speed;
    }

    public void StartMoving()
    {
        move = true;
    }

    private void Update()
    {
        if (!move) return;

        if (transform.position.x < leftCameraEdge)
        {
            DOTween.Kill(transform);
            Destroy(this.gameObject);
            return;
        }
        if (this.gameObject == null)
        {
            return;
        }

        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Bullet"))
        {
            var bullet = collision.GetComponent<PlayerBullet>();
            if (bullet != null && (bullet.weaponName == "Sledgehammer" || bullet.weaponName == "Derrick Kick"))
            {
                if (bullet.weaponName == "Sledgehammer")
                {
                    Instantiate(iadeiBullet, transform.position, Quaternion.identity);
                }
                DOTween.Kill(transform);
                Destroy(this.gameObject);
            }
        }
    }
}
