using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombi_LaneChanger : MonoBehaviour
{
    public void ChangeLane(int laneToGo, float timeForFloat, bool shouldItTab)
    {
        if (shouldItTab)
        {
            float tabDistance = 0.5f;

            transform.DOMoveY(LaneFinder.laneYPositions[laneToGo] + tabDistance, timeForFloat * 0.75f).OnComplete(() => {
                DOVirtual.DelayedCall(timeForFloat * 0.15f, () =>
                {
                    transform.DOMoveY(LaneFinder.laneYPositions[laneToGo], timeForFloat * 0.1f).OnComplete(() => {

                    });
                });
            });
        }
        else
        {
            transform.DOMoveY(LaneFinder.laneYPositions[laneToGo], timeForFloat).OnComplete(() => {
                Debug.Log("Hareket tamamlandý!");
            });
        }
    }
}
