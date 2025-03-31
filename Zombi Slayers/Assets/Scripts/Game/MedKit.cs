using UnityEngine;

public class MedKit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Health>().TakeMedKit();
            Destroy(gameObject);
        }
    }
}
