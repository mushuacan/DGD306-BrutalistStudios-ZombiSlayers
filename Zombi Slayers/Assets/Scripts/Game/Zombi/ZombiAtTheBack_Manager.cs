using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class ZombiAtTheBack_Manager : MonoBehaviour
{
    public GameObject zombiAtTheBackPrefab;
    public List<GameObject> zombiesInLane1;
    public List<GameObject> zombiesInLane2;
    public List<GameObject> zombiesInLane3;

    public bool addZombiRandomly;
    public float newZombiDelay;
    private bool gameEnded;
    public bool stopZombying;
    public bool stopZombyingIfOnlyDerrick;

    void Start()
    {
        if (stopZombying) return;
        gameEnded = false;
        AddBackZombi(1);
        AddBackZombi(2);
        AddBackZombi(3);
        if (addZombiRandomly) AddZombiRandomly(newZombiDelay);

    }

    public void StopIfOnlyDerrick()
    {
        if (stopZombyingIfOnlyDerrick) stopZombying = true;
    }

    private void AddZombiRandomly(float time)
    {
        if (gameEnded) return;
        AddBackZombi(Random.Range(1,4));
        DOVirtual.DelayedCall(time, () => { AddZombiRandomly(time); });
    }

    public void AddBackZombi(int lane)
    {
        if (gameEnded) return;
        if (stopZombying) return;
        GameObject zombiATB = Instantiate(zombiAtTheBackPrefab);
        zombiATB.transform.SetParent(gameObject.transform);
        int order = SetOrderOfZombi(lane, zombiATB);
        zombiATB.GetComponent<ZombiAtTheBack>().SetPosition(lane, order);
    }

    public void EndGame(float movementSpeed, float transitionDuration)
    {
        gameEnded = true;
        foreach (GameObject obje in zombiesInLane1)
        {
            obje.GetComponent<ZombiAtTheBack>().EndGame(movementSpeed, transitionDuration);
        }
        foreach (GameObject obje in zombiesInLane2)
        {
            obje.GetComponent<ZombiAtTheBack>().EndGame(movementSpeed, transitionDuration);
        }
        foreach (GameObject obje in zombiesInLane3)
        {
            obje.GetComponent<ZombiAtTheBack>().EndGame(movementSpeed, transitionDuration);
        }
    }

    private int SetOrderOfZombi(int lane, GameObject zombiATB)
    {
        if (lane == 1)
        {
            zombiesInLane1.Add(zombiATB);
            return zombiesInLane1.Count;
        }
        else if (lane == 2)
        {
            zombiesInLane2.Add(zombiATB);
            return zombiesInLane2.Count;
        }
        else if (lane == 3)
        {
            zombiesInLane3.Add(zombiATB);
            return zombiesInLane3.Count;
        }
        else
        {
            return -5;
        }
    }

    public int GetZombiCount()
    {
        int zombi = 0;
        zombi += zombiesInLane1.Count + zombiesInLane2.Count + zombiesInLane3.Count;
        return zombi;
    }
}
