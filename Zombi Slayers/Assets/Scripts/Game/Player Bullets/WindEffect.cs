using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    private int lane;
    private void OnEnable()
    {
        Destroy(gameObject, 1f);
    }
    public void ArrangeLane(int laner)
    {
        lane = laner;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombi"))
        {
            collision.GetComponent<Zombi_LaneChanger>().ChangeLane(lane, 1, true);
        }
    }
}
