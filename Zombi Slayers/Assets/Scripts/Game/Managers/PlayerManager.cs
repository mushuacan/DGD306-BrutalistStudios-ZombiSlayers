using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public List<Scriptable_PlayerCharacter> all_Characters = new List<Scriptable_PlayerCharacter>();
    public List<Scriptable_PlayerCharacter> Woods_all = new List<Scriptable_PlayerCharacter>();
    public List<Scriptable_PlayerCharacter> Fletchers_all = new List<Scriptable_PlayerCharacter>();
    public List<GameObject> players = new List<GameObject>();

    private LevelMaker levelMaker;

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
                players[playerCount - 1].name = "Player Unneeded";
                PlayerCounter();
            }
            pssb.ArrangePlayerUI(playerCount, this.gameObject);
        }

        player.GetComponent<Player_Movement>().animations = (bool)GameSettings.Instance.settings["animations"];
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
        DOVirtual.DelayedCall(0.25f, ArrangeStarterPacks);
    }
    private void ArrangeStarterPacks()
    {
        foreach (var player in players)
        {
            Player_Movement player_Movement = player.GetComponent<Player_Movement>();
            player_Movement.isPlayingNow = true;
            player_Movement.StarterPack();
        }
    }
    private void ClearPlayers()
    {
        foreach (var player in players)
        {
            if (player.CompareTag("Untagged"))
            {
                Destroy(player);
            }
        }
        PlayerCounter();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearPlayers();
        levelMaker = FindObjectOfType<LevelMaker>();
        if (levelMaker != null)
        {
            NewLevelOpened(levelMaker.level);
        }
    }
    //public void NewSceneLoaded()
    //{
    //    ClearPlayers();
    //    levelMaker = FindObjectOfType<LevelMaker>();
    //    if (levelMaker != null)
    //    {
    //        NewLevelOpened(levelMaker.level);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("LevelMaker bulunamadý.");
    //    }
    //}
}
