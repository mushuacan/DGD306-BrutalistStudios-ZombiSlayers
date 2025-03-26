using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health : MonoBehaviour
{
    private float health;
    [SerializeField] private int lane;
    [SerializeField] private float leftCameraEdge;
    [SerializeField] private ZombiAtTheBack_Manager zombiATBM;
    [SerializeField] private ZombiCharacter zombiChar;


    private void OnEnable()
    {
        health = zombiChar.zombi.health;
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
    }

    private void Update()
    {
        if (transform.position.x < leftCameraEdge)
        {
            if (zombiATBM != null)
            {
                zombiATBM.AddBackZombi(lane);
            }
            Destroy(this.gameObject);
        }
    }
}
