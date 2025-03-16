using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health : MonoBehaviour
{
    private float health;
    [SerializeField] private Scriptable_Zombies zombi;


    private void OnEnable()
    {
        health = zombi.health;
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
                Destroy(this.gameObject);
            }
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().GiveDamage();
        }
    }
}
