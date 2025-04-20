using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombi"))
        {
            collision.GetComponent<Zombi_LaneChanger>().ChangeLane(3, 1, false);
        }
    }
}
