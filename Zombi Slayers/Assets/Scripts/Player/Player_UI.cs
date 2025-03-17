using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private Slider castTimer;
    [SerializeField] private RawImage heart1;
    [SerializeField] private RawImage heart2;
    [SerializeField] private RawImage heart3;
    [SerializeField] private RawImage heart4;

    // Start is called before the first frame update
    void Start()
    {
        castTimer.gameObject.SetActive(false);
    }

    public void StartCastTimer(float time)
    {
        castTimer.gameObject.SetActive(true);
        castTimer.value = 0;
        castTimer.DOValue(1, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            castTimer.gameObject.SetActive(false);
            castTimer.value = 0;
        });
    }
    public void ArrangeHearts(int hearts)
    {
        if (hearts > 0)
        {
            heart1.gameObject.SetActive(true);
        } else
        {
            heart1.gameObject.SetActive(false);
        }
        if (hearts > 1)
        {
            heart2.gameObject.SetActive(true);
        }
        else
        {
            heart2.gameObject.SetActive(false);
        }
        if (hearts > 2)
        {
            heart3.gameObject.SetActive(true);
        }
        else
        {
            heart3.gameObject.SetActive(false);
        }
        if (hearts > 3)
        {
            heart4.gameObject.SetActive(true);
        }
        else
        {
            heart4.gameObject.SetActive(false);
        }

    }
}
