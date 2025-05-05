using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public GameObject PlayerInputManagerPrefab;
    public PlayerManager playerManager;
    public GameObject player1;
    public Player_Inputs player1_inputs;
    public GameObject player2;
    public Player_Inputs player2_inputs;

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

    private void Update()
    {
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

    public void ArrangePlayerUI(int players, GameObject PlayerManagerReferans)
    {
        playerManager = PlayerManagerReferans.GetComponent<PlayerManager>();

        if (players > 0)
        {
            Player1_Waiting.SetActive(false);
            Player1_Arrived.SetActive(true);
            player1 = GameObject.Find("Player 1");
            player1.GetComponent<Player_Character>().character = playerManager.all_Characters[0];
            player1_inputs = player1.GetComponent<Player_Inputs>();
        }
        if(players > 1)
        {
            Player2_Waiting.SetActive(false);
            Player2_Arrived.SetActive(true);
            player2 = GameObject.Find("Player 2");
            player2.GetComponent<Player_Character>().character = playerManager.all_Characters[1];
            player2_inputs = player2.GetComponent<Player_Inputs>();
        }
    }
    private void SwitchMenus()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
        Instantiate(PlayerInputManagerPrefab);
    }

}
