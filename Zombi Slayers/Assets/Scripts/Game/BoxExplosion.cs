using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        DOVirtual.DelayedCall(0.25f, () => Destroy(gameObject));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().GiveDamage();
        }
        else if (collision.CompareTag("Obstacle") || collision.CompareTag("Box") || collision.CompareTag("Zombi") 
            || collision.CompareTag("Zombi_Bullet") || collision.CompareTag("Supply") || collision.CompareTag("Player_Bullet"))
        {
            DOTween.Kill(collision.gameObject);

            foreach (Transform child in collision.transform)
            {
                DOTween.Kill(child.gameObject);
            }

            Destroy(collision.gameObject);

        }
    }
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
