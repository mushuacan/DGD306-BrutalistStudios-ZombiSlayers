using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Push : MonoBehaviour
{
    public void PushBack(bool movex, bool movey)
    {
        if ( movex )
        transform.DOMoveX(transform.position.x + 1, 0.8f);
        
        if ( movey )
        transform.DOMoveY(transform.position.y + 0.5f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            transform.DOMoveY(transform.position.y - 0.5f, 0.4f).SetEase(Ease.InOutQuad);
        });
    }
}
