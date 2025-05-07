using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : AlarmSystem
{
    [SerializeField] private Player_Movement player1;
    [SerializeField] private Player_Movement player2;

    public int PlayerCount;

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
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!contuniu) return;

        if (PlayerCount == 2)
        {
            if (player1.state == Player_Movement.StateOC.Dead && player2.state == Player_Movement.StateOC.Dead)
            {
                StartTheGameFromScratch();
            }
        }
        if (PlayerCount == 1) 
        {
            if (player1.state == Player_Movement.StateOC.Dead)
            {
                StartTheGameFromScratch();
            }
        }
    }

    private void StartTheGameFromScratch()
    {
        Debug.Log("Sahne baþtan baþlatýlýyor.");
        DOTween.KillAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
