using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiBullet : MonoBehaviour
{
    private float speed;
    private float damage;
    private float duration;

    public void Arrangements(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public void StartMoving()
    {
        transform.DOMoveX(transform.position.x + (speed * duration), duration).SetEase(Ease.Linear).OnComplete(() => { StartMoving(); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().GiveDamage(damage);
        }
    }
}
