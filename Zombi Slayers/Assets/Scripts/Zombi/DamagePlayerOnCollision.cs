using UnityEngine;
using DG.Tweening;

public class DamagePlayerOnCollision : MonoBehaviour
{
    [SerializeField] private bool destroyOnCollision;
    [Tooltip("For only bullets")]
    [SerializeField] private bool canSlideable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Movement pMove = collision.GetComponent<Player_Movement>();

            if (pMove.state == Player_Movement.StateOC.Dead)
            {
                return;
            }
            if (pMove.action == Player_Movement.ActionOC.Sliding && canSlideable)
            {
                return;
            }

            collision.GetComponent<Player_Health>().GiveDamage();

            if (destroyOnCollision)
            {
                DOTween.Kill(transform);
                Destroy(gameObject); 
            }
        }
    }
}
