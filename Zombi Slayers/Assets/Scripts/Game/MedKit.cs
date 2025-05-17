using UnityEngine;

public class MedKit : MonoBehaviour
{
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
                    Destroy(gameObject);
                }
            }
        }
    }
}
