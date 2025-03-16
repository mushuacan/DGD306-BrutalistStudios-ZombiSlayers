using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public bool isBreakable;
    public bool isSlideable;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSlideable && collision.GetComponent<Player_Movement>().action == Player_Movement.ActionOC.Sliding )
            { // eðer oyuncu slide yapýyorsa ve bu engel slideable ise hasar verme
                return;
            }
            collision.GetComponent<Player_Health>().GiveDamage();
        }

        if (isBreakable && collision.CompareTag("Player_Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
