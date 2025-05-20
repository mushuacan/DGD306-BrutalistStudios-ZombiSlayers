using DG.Tweening;
using UnityEngine;

public class Zombi_BOSS_Jumped : MonoBehaviour
{
    public LaneFinder laneFinder;
    public int lane;

    private void Start()
    {
    }

    private void ChangeLane(int laneToChange, float timer)
    {
        laneFinder.lane = laneToChange;
        transform.DOMoveY(LaneFinder.laneYPositions[laneToChange], timer).SetEase(Ease.OutQuad).OnComplete(() => lane = laneToChange);
    }
    private void ChangeXPosition(float xPoz, float timer)
    {
        transform.DOMoveX(transform.position.x + xPoz, timer);
    }
    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    public void Savrul(int bossesLane)
    {
        if (laneFinder != null)
        {
            lane = laneFinder.lane;
        }
        if (bossesLane == 1)
        {
            if (lane == 1)
            {
                ChangeLane(2, 1f);
                ChangeXPosition(1, 1f);
            }
            if (lane == 2)
            {
                ChangeLane(3, 1f);
                ChangeXPosition(1, 1f);
            }
            if (lane == 3)
            {
                if (transform.position.x < 0)
                {
                    ChangeLane(1, 1.5f);
                    ChangeXPosition(2, 1f);
                }
                else
                {
                    ChangeLane(2, 1f);
                    ChangeXPosition(1, 1f);
                }
            }
        }
        if (bossesLane == 2)
        {
            if (lane == 2)
            {
                ChangeLane(1, 1f);
                ChangeXPosition(1, 1f);
            }
            if (lane == 3)
            {
                ChangeLane(2, 1f);
                ChangeXPosition(1, 1f);
            }
            if (lane == 1)
            {
                if (transform.position.x < 0)
                {
                    ChangeLane(0, 5f);
                }
                else 
                {
                    ChangeLane(2, 1.5f);
                    ChangeXPosition(1, 1.5f);
                }
            }
        }
    }
}
