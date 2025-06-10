using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWarning : MonoBehaviour
{
    public GameObject supplyer;
    public GameObject obstacler;
    public GameObject bulleter;
    public GameObject zombier;

    private bool supply;
    private bool obstacle;
    private bool bullet;
    private bool zombi;
    private float warningersValue;

    private void Start()
    {
        bulleter.SetActive(false);
        obstacler.SetActive(false);
        supplyer.SetActive(false);
        zombier.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((float)GameSettings.Instance.settings["warningers"] == warningersValue)
        {
            return;
        }
        else
        {
            warningersValue = (float)GameSettings.Instance.settings["warningers"];
        }
        if (warningersValue == 0f)
        {
            supply = false;
            obstacle = false;
            bullet = false;
            zombi = false;
        }
        else if (warningersValue == 0.25f)
        {
            supply = false;
            obstacle = false;
            bullet = true;
            zombi = false;
        }
        else if (warningersValue == 0.5f)
        {
            supply = false;
            obstacle = false;
            bullet = true;
            zombi = true;
        }
        else if (warningersValue == 0.75f)
        {
            supply = true;
            obstacle = false;
            bullet = true;
            zombi = false;
        }
        else if (warningersValue == 1)
        {
            supply = true;
            obstacle = true;
            bullet = true;
            zombi = true;
        }
        else
        {
            supply = false;
            obstacle = false;
            bullet = false;
            zombi = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Supply") && supply)
        {
            supplyer.SetActive(true);
        }
        if (collision.CompareTag("Obstacle") && obstacle)
        {
            obstacler.SetActive(true);
        }
        if (collision.CompareTag("Zombi_Bullet") && bullet)
        {
            bulleter.SetActive(true);
        }
        if (collision.CompareTag("Zombi") && zombi)
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
