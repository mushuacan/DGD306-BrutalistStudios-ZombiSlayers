using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject ESCmenu;
    public GameObject endMenu;

    void Start()
    {
        endMenu.SetActive(false);
        ESCmenu.SetActive(false);
    }
    public void OpenEndMenu(bool becauseOfWin)
    {
        endMenu.GetComponent<EndGameMenu>().isItAWin = becauseOfWin;
        endMenu.SetActive(true);
    }
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
