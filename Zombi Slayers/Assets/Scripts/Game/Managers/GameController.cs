using UnityEngine;
using DG.Tweening;

public class GameController : AlarmSystem
{
    [SerializeField] private Player_Movement player1;
    [SerializeField] private Player_Movement player2;

    public int PlayerCount;

    private bool canDebug;

    // Start is called before the first frame update
    void Awake()
    {
        if (player1 == null)
        {
            Alarm("Oyuncu 1 Referansý Atanmadý", AlarmTypes.Important);
        }
        if (player2 == null && PlayerCount == 2)
        {
            Alarm("Oyuncu 2 Referansý Atanmadý", AlarmTypes.Important);
        }
        canDebug = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!contuniu) return;


        if (PlayerCount == 2)
        {
            if (player1.state == Player_Movement.StateOC.Dead && player2.state == Player_Movement.StateOC.Dead && canDebug)
            {
                canDebug = false;
                Alarm("Oyunun bitmiþ olmasý gerek", AlarmTypes.Bad);
                ChangeCanDebug();
            }
        }
        if (PlayerCount == 1) 
        {
            if (player1.state == Player_Movement.StateOC.Dead && canDebug && PlayerCount == 1)
            {
                canDebug = false;
                Alarm("Oyunun bitmiþ olmasý gerek", AlarmTypes.Bad);
                ChangeCanDebug();
            }
        }
    }

    private void ChangeCanDebug()
    {
        DOVirtual.DelayedCall(1, () => { canDebug = true; } );
    }
}
