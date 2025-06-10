using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWarning : MonoBehaviour
{
    public GameObject supplyer;
    public GameObject obstacler;
    public GameObject bulleter;
    public GameObject zombier;


    private void Start()
    {
        bulleter.SetActive(false);
        obstacler.SetActive(false);
        supplyer.SetActive(false);
        zombier.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Supply"))
        {
            supplyer.SetActive(true);
        }
        if (collision.CompareTag("Obstacle"))
        {
            obstacler.SetActive(true);
        }
        if (collision.CompareTag("Zombi_Bullet"))
        {
            bulleter.SetActive(true);
        }
        if (collision.CompareTag("Zombi"))
        {
            zombier.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Supply"))
        {
            supplyer.SetActive(false);
        }
        if (collision.CompareTag("Obstacle"))
        {
            obstacler.SetActive(false);
        }
        if (collision.CompareTag("Zombi_Bullet"))
        {
            bulleter.SetActive(false);
        }
        if (collision.CompareTag("Zombi"))
        {
            zombier.SetActive(false);
        }
    }
}
