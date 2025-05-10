using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreake : MonoBehaviour
{
    public Scriptable_BoxInsiders insider;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return; // Eðer zaten tetiklendiyse, iþlemi durdur

        if (collision.CompareTag("Player_Bullet"))
        {
            isTriggered = true; // Tetiklendi olarak iþaretle

            Instantiate(insider.prefab, transform.position, transform.rotation, transform.parent);
            Destroy(this.gameObject);
        }
    }

}
