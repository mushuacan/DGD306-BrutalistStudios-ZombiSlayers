using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    public List<Scriptable_PlayerCharacter> all_Characters = new List<Scriptable_PlayerCharacter>();
    public List<Scriptable_PlayerCharacter> Woods_all = new List<Scriptable_PlayerCharacter>();
    public List<Scriptable_PlayerCharacter> Fletchers_all = new List<Scriptable_PlayerCharacter>();
    private List<GameObject> players = new List<GameObject>();


    public int playerCount;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Oyuncu eklendi: " + player.name);

        PlayerCounter();

        for (int i = 0; i < playerCount; i++)
        {
            players[i].name = "Player " + (i + 1);
        }

        PlayerSelectionSceneButtons pssb = FindObjectOfType<PlayerSelectionSceneButtons>();

        if (pssb != null)
        {
            if (playerCount > pssb.playerCount)
            {
                players[playerCount - 1].tag = "Untagged";
                PlayerCounter();
            }
            pssb.ArrangePlayerUI(playerCount, this.gameObject);
        }
    }

    private void PlayerCounter()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        playerCount = players.Count;
    }

    public void NewLevelOpened(int level)
    {
        foreach (var player in players)
        {
            string charName = player.GetComponent<Player_Character>().character.characterName;
            if (charName == "Woods")
            {
                player.GetComponent<Player_Character>().character = Woods_all[level];
            }
            if (charName == "Fletcher")
            {
                player.GetComponent<Player_Character>().character = Fletchers_all[level];
            }
        }
    }
}
