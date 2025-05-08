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
            if (pMove.action == Player_Movement.ActionOC.SecondAbility && canSlideable && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
            {
                return;
            }

            Player_Health player_Health = collision.GetComponent<Player_Health>();

            if (player_Health.undamageableDelay < Time.timeSinceLevelLoad)
            {
                player_Health.GiveDamage();

                if (destroyOnCollision)
                {
                    DOTween.Kill(transform);
                    Destroy(gameObject);
                }
            }
        }
    }
}
