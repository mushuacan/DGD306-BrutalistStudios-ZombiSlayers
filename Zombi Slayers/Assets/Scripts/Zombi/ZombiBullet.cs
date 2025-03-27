using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float leftCameraEdge;

    public void Arrangements(float speed, float damage)
    {
        this.speed = speed;
    }

    public void StartMoving()
    {
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
        transform.DOMoveX(transform.position.x - (speed * 1), 1).SetEase(Ease.Linear).OnComplete(() => { StartMoving(); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().GiveDamage();
        }
        if (collision.CompareTag("Player_Bullet"))
        {
            if (collision.GetComponent<PlayerBullet>().weaponName == "Sledgehammer")
            {
                DOTween.Kill(transform);
                Destroy(this.gameObject);
            }
        }
    }
}
