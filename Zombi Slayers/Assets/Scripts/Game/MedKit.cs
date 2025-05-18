using DG.Tweening;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Health playerHealth = collision.GetComponent<Player_Health>();
            if (playerHealth != null )
            {
                if (playerHealth.health < 4 && playerHealth.health != 0)
                {
                    playerHealth.TakeMedKit();
                    collider2d.enabled = false;
                    transform.DOScale(Vector3.zero, 0.5f).OnComplete( () => Destroy(gameObject));
                }
            }
        }
    }
}
