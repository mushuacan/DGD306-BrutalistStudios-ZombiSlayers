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
            Destroy(this.gameObject);
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
    }
}
