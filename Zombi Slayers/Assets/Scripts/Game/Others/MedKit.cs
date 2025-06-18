using DG.Tweening;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    private Sequence flashSequence;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Health playerHealth = collision.GetComponent<Player_Health>();
            if (playerHealth != null )
            {
                if (playerHealth.health < 4 && playerHealth.health != 0)
                {
                    if ((float)GameSettings.Instance.settings["difficulty"] == 0 && playerHealth.health == 1)
                        playerHealth.TakeMedKit();
                    playerHealth.TakeMedKit();
                    collider2d.enabled = false;

                    Destroy(gameObject);
                }
            }
        }
    }
}
