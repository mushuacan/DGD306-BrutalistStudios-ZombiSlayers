using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerSelectionSceneButtons : MonoBehaviour
{
    public int playerCount;
    public string gameType;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject Player1_Waiting;
    public GameObject Player1_Arrived;
    public GameObject Player2_Waiting;
    public GameObject Player2_Arrived;
    public GameObject FirstSelectedButton;

    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);


        Player1_Waiting.SetActive(true);
        Player1_Arrived.SetActive(false);
        Player2_Waiting.SetActive(true);
        Player2_Arrived.SetActive(false);
    }

    public void Singleplayer()
    {
        playerCount = 1;
        gameType = "Singleplayer";
        SwitchMenus();
    }

    public void Multiplayer()
    {
        playerCount = 2;
        gameType = "Multiplayer";
        SwitchMenus();
    }

    public void EndlessRun()
    {
        playerCount = 1;
        gameType = "EndlessRun";
        SwitchMenus();
    }

    public void ArrangePlayerUI(int players)
    {
        if (players > 0)
        {
            Player1_Waiting.SetActive(false);
            Player1_Arrived.SetActive(true);
        }
        if(players > 1)
        {
            Player2_Waiting.SetActive(false);
            Player2_Arrived.SetActive(true);
        }
    }
    private void SwitchMenus()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

}
