using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreake : MonoBehaviour
{
    public Scriptable_BoxInsiders insider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Bullet"))
        {
            Instantiate(insider.prefab, transform.position, transform.rotation, transform.parent);

            Destroy(this.gameObject);
        }
    }
}
