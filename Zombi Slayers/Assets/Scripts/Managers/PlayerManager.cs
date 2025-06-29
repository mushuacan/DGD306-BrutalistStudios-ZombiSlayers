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
    public List<Scriptable_PlayerCharacter> Derricks_all = new List<Scriptable_PlayerCharacter>();
    public List<GameObject> players = new List<GameObject>();

    private LevelMaker levelMaker;

    public int playerCount;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // �lk olan� sahne ge�i�inde koru
    }

    public void OnPlayerJoined(PlayerInput player = null)
    {
        if (player != null)
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
                Destroy(players[playerCount - 1]);
                PlayerCounter();
            }
            pssb.ArrangePlayerUI(playerCount, this.gameObject);
        }
    }

    private void PlayerCounter()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        playerCount = players.Count;
        foreach (GameObject player in players)
        {
            player.GetComponent<Player_Movement>().animations = (bool)GameSettings.Instance.settings["animations"];
        }
    }

    public void NewLevelOpened(int level)
    {
        // Player_Movement bile�enine sahip t�m objeleri bul
        Player_Movement[] movementComponents = FindObjectsOfType<Player_Movement>();

        foreach (Player_Movement pm in movementComponents)
        {
            GameObject obj = pm.gameObject;

            if (obj.name == "Player_a1")
            {
                Destroy(obj);
                Debug.Log("Destroyed object: " + obj.name);
            }
        }
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
            if (charName == "Derrick")
            {
                player.GetComponent<Player_Character>().character = Derricks_all[level];
            }
        }
        DOVirtual.DelayedCall(0.005f, ArrangeStarterPacks);
    }
    private void ArrangeStarterPacks()
    {
        foreach (var player in players)
        {
            Player_Movement player_Movement = player.GetComponent<Player_Movement>();
            player_Movement.isPlayingNow = true;
            player_Movement.StarterPack();
        }
        Debug.Log("Starter Packs Arranged.");
    }
    private void ClearPlayers()
    {
        Debug.Log("Oyuncualr temizleniyor");
        if (players != null && players.Count > 0)
        {
            foreach (var player in players)
            {
                Debug.Log("Oyunculara bak�l�yor " + player);
                if (player != null && player.CompareTag("Untagged"))
                {
                    Debug.Log("Oyuncu silindi: "  + player);
                    Destroy(player);
                }
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
        if (scene.name == "Player Selection Scene" && players.Count > 0)
        {
            // Ge�ici liste yaparak null olmayanlar� g�venle sil
            var validPlayers = players.FindAll(p => p != null);

            foreach (var player in validPlayers)
            {
                Destroy(player);
            }

            players.Clear();

            // Kendi objeni biraz gecikmeyle sil (opsiyonel g�venlik)
            Destroy(this.gameObject, 0.1f);
        }

        ClearPlayers();
        levelMaker = FindObjectOfType<LevelMaker>();
        if (levelMaker != null)
        {
            NewLevelOpened(levelMaker.level);
        }
    }

    public void KillAllPlayers()
    {
        foreach (var player in players)
        {
            Player_Health player_Health = player.GetComponent<Player_Health>();
            if (player_Health != null)
            {
                player_Health.KillYourself();
            }
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
    //        Debug.LogWarning("LevelMaker bulunamad�.");
    //    }
    //}
}
