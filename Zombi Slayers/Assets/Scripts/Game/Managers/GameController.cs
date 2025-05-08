using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.TextCore.Text;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player_Movement player1;
    [SerializeField] private Player_Movement player2;

    public int PlayerCount;

    private bool Starting;

    private void Start()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            PlayerCount = playerManager.playerCount;
            if (PlayerCount == 2)
            {
                player2 = playerManager.players[1].GetComponent<Player_Movement>();
            }
            player1 = playerManager.players[0].GetComponent<Player_Movement>();

            if (player1 != null)
                player1.GetComponent<Player_Health>().OnPlayerDied += OnListenPlayerDied;
            if (player2 != null)
                player2.GetComponent<Player_Health>().OnPlayerDied += OnListenPlayerDied;
        }
    }
    void OnDestroy()
    {
        if (player1 != null)
            player1.GetComponent<Player_Health>().OnPlayerDied -= OnListenPlayerDied;
        if (player2 != null)
            player2.GetComponent<Player_Health>().OnPlayerDied -= OnListenPlayerDied;
    }
    // Update is called once per frame
    void OnListenPlayerDied()
    {
        if (PlayerCount == 2)
        {
            if (player1.state == Player_Movement.StateOC.Dead && player2.state == Player_Movement.StateOC.Dead)
            {
                Starting = true;
                StartTheGameFromScratch();
            }
        }
        if (PlayerCount == 1) 
        {
            if (player1.state == Player_Movement.StateOC.Dead)
            {
                Starting = true;
                StartTheGameFromScratch();
            }
        }
    }

    private void StartTheGameFromScratch()
    {
        Debug.Log("Sahne baþtan baþlatýlýyor.");
        Debug.Log(Starting);
        DOTween.KillAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
