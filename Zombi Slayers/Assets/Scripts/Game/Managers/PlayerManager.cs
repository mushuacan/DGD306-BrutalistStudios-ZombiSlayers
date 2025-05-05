using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    public List<Scriptable_PlayerCharacter> Woods_all = new List<Scriptable_PlayerCharacter>();
    public List<Scriptable_PlayerCharacter> Fletchers_all = new List<Scriptable_PlayerCharacter>();

    private GameObject[] players;

    public int playerCount;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayerCreated()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); 
        playerCount = players.Length;

        PlayerSelectionSceneButtons pssb = FindObjectOfType<PlayerSelectionSceneButtons>();

        if (pssb != null)
        {
            if (playerCount > pssb.playerCount)
            {
                players[playerCount - 1].SetActive(false);
            }
        }
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
