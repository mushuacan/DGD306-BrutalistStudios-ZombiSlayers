using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Magazines : MonoBehaviour
{
    [SerializeField] private int magazineBulletCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Attack player_Attack = collision.GetComponent<Player_Attack>();
            if (player_Attack != null)
            {
                player_Attack.TakeMagazine(magazineBulletCount);
                Destroy(this.gameObject);
            }
        }
    }
}
