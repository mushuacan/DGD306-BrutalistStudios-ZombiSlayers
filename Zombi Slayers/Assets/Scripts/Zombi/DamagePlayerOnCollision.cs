using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnCollision : MonoBehaviour
{
    [SerializeField] private bool destroyOnCollision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().GiveDamage();
            if (destroyOnCollision) { Destroy(gameObject); }
        }
    }
}
