using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuManagerInGame : MonoBehaviour
{
    public GameObject ESCmenu;

    public void OpenESCMenu()
    {
        Time.timeScale = 0.0f;
        ESCmenu.SetActive(true);
    }
    public void CloseESCMenu()
    {
        Time.timeScale = 1.0f;
        ESCmenu.SetActive(false);
    }

}
