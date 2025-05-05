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
    public GameObject FirstSelectedButton;

    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);
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

    private void SwitchMenus()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

}
