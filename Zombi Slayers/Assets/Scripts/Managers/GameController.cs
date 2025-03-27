using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : AlarmSystem
{
    [SerializeField] private Player_Movement player1;
    [SerializeField] private Player_Movement player2;

    private bool canDebug;

    // Start is called before the first frame update
    void Awake()
    {
        if (player1 == null)
        {
            Alarm("Oyuncu 1 Referans� Atanmad�", AlarmTypes.Important);
        }
        if (player2 == null)
        {
            Alarm("Oyuncu 2 Referans� Atanmad�", AlarmTypes.Important);
        }
        canDebug = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!contuniu) return;

        if (player1.state == Player_Movement.StateOC.Dead && player2.state == Player_Movement.StateOC.Dead && canDebug)
        {
            canDebug = false;
            Alarm("Oyunun bitmi� olmas� gerek", AlarmTypes.Bad);
            ChangeCanDebug();
        }
    }

    private void ChangeCanDebug()
    {
        DOVirtual.DelayedCall(1, () => { canDebug = true; } );
    }
}
