using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombi_LaneChanger : MonoBehaviour
{
    [SerializeField] private LaneFinder _laneFinder;
    public void ChangeLane(int laneToGo, float timeForFloat, bool shouldItTab)
    {
        if (shouldItTab)
        {
            float tabDistance = 0.25f;

            transform.DOMoveY(LaneFinder.laneYPositions[laneToGo] + tabDistance, timeForFloat * 0.75f).OnComplete(() => {
                DOVirtual.DelayedCall(timeForFloat * 0.15f, () =>
                {
                    transform.DOMoveY(LaneFinder.laneYPositions[laneToGo], timeForFloat * 0.1f).OnComplete(() => {
                        _laneFinder.MakeLane(laneToGo);
                    });
                });
            });
        }
        else
        {
            transform.DOMoveY(LaneFinder.laneYPositions[laneToGo], timeForFloat).SetEase(Ease.Linear).OnComplete(() => {
                Debug.Log("Hareket tamamlandý!");
                _laneFinder.MakeLane(laneToGo);
            });
        }
    }
}
