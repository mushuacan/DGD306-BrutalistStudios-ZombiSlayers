using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : AlarmSystem
{
    [SerializeField] private Player_Movement player1;
    [SerializeField] private Player_Movement player2;

    // Start is called before the first frame update
    void Awake()
    {
        if (player1 == null)
        {
            Alarm("Oyuncu 1 Referansý Atanmadý", AlarmTypes.Important);
        }
        if (player2 == null)
        {
            Alarm("Oyuncu 2 Referansý Atanmadý", AlarmTypes.Important);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!contuniu) return;

        if (player1.state == Player_Movement.StateOC.Dead && player2.state == Player_Movement.StateOC.Dead)
        {
            Alarm("Oyunun bitmiþ olmasý gerek", AlarmTypes.Bad);
        }
    }
}
