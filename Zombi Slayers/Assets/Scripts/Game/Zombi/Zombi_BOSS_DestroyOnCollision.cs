using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_BOSS_DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Zombi") || collision.CompareTag("Zombi_Bullet") || collision.CompareTag("Supply") || collision.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
        }
    }
}
