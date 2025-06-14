using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class ZombiAtTheBack_Manager : MonoBehaviour
{
    public GameObject zombiAtTheBackPrefab;
    public List<GameObject> zombiesInLane1;
    public List<GameObject> zombiesInLane2;
    public List<GameObject> zombiesInLane3;
    public GameObject[] zombiLanes;

    public bool addZombiRandomly;
    public float newZombiDelay;
    private bool gameEnded;
    public bool stopZombying;
    public bool stopZombyingIfOnlyDerrick;
    private int zombiAddedLastAt;


    public float spaceBetweenZombis;
    public float leftCameraBorder;
    public float lanePositioningDuration;

    void Start()
    {
        zombiAddedLastAt = 0;
        if (stopZombying) return;
        gameEnded = false;
        AddBackZombi(1, false);
        AddBackZombi(2, false);
        AddBackZombi(3, false);
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
        DOVirtual.DelayedCall(time, () => { AddZombiRandomly(time); })
                .SetUpdate(false); // false => scaled time kullan, yani oyun durduðunda dur

    }

    public void AddBackZombi(int lane, bool checkForHardness = true)
    {
        if (checkForHardness) if (!IsDifficultyOkeyWithAddingNewZombi()) return;
        if (gameEnded) return;
        if (stopZombying) return;

        if (lane == 1 && zombiesInLane1.Count > 12) return;
        if (lane == 2 && zombiesInLane2.Count > 12) return;
        if (lane == 3 && zombiesInLane3.Count > 12) return;

        GameObject zombiATB = Instantiate(zombiAtTheBackPrefab);
        zombiATB.transform.SetParent(zombiLanes[lane].transform);
        int order = SetOrderOfZombi(lane, zombiATB);
        zombiATB.GetComponent<ZombiAtTheBack>().SetPosition(lane, order, zombiLanes[lane].transform.position.x);
        SetPositionOfLaner(lane);
    }

    public void SetPositionOfLaner(int lane)
    {
        //myTween.Kill();
        int Counter = 0;
        if (lane == 1) { Counter = zombiesInLane1.Count; }
        if (lane == 2) { Counter = zombiesInLane2.Count; }
        if (lane == 3) { Counter = zombiesInLane3.Count; }
        float orderPosition = leftCameraBorder + Counter * spaceBetweenZombis;
        zombiLanes[lane].transform.DOMoveX(orderPosition, lanePositioningDuration).SetEase(Ease.OutQuad);
    }

    public void EndGame(float movementSpeed, float transitionDuration)
    {
        gameEnded = true;
        float xPosition = transform.position.x - (movementSpeed / 2) * transitionDuration;
        zombiLanes[1].transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InOutQuad);
        zombiLanes[2].transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InOutQuad);
        zombiLanes[3].transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InOutQuad);
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


    public bool IsDifficultyOkeyWithAddingNewZombi()
    {
        zombiAddedLastAt++;
        if ((float)GameSettings.Instance.settings["difficulty"] == 0f)
        {
            if (zombiAddedLastAt > 1)
            {
                zombiAddedLastAt = 0;
                return true;
            }
            else return false;
        }
        else if ((float)GameSettings.Instance.settings["difficulty"] == 0.5f)
        {
            if (zombiAddedLastAt > 0)
            {
                zombiAddedLastAt = 0;
                return true;
            }
            else return false;
        }
        else if ((float)GameSettings.Instance.settings["difficulty"] == 1f)
        {
            return true;
        }
        else
        {
            return true;
        }
    }
}
