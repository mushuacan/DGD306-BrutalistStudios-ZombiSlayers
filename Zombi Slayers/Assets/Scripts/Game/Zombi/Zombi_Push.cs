using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Push : MonoBehaviour
{
    public void PushBack(bool movex, bool movey)
    {
        if ( movex )
        transform.DOMoveX(transform.position.x + 2, 0.8f).SetEase(Ease.Linear);
        
        if ( movey )
        transform.DOMoveY(transform.position.y + 0.5f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            if (transform != null)
            transform.DOMoveY(LaneFinder.laneYPositions[this.GetComponent<LaneFinder>().lane], 0.4f).SetEase(Ease.InOutQuad);
        });
    }
}
