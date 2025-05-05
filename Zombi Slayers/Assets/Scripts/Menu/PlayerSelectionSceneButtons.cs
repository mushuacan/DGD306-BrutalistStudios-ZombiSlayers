using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    public GameObject player1, player2;
    public Player_Inputs player1_inputs, player2_inputs;
    public Player_Character player1_character, player2_character;

    public RawImage player1_image, player2_image;
    public TextMeshProUGUI player1_text, player2_text;

    private bool p1_changed, p2_changed;

    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);


        Player1_Waiting.SetActive(true);
        Player1_Arrived.SetActive(false);
        Player2_Waiting.SetActive(true);
        Player2_Arrived.SetActive(false);

        p1_changed = false;
        p2_changed = false;
    }

    private void Update()
    {
        if (player1_inputs != null)
        {
            if (player1_inputs.MovementValues.x > 0.5)
            {
                if (p1_changed) return;
                ArrangeCharacter(1, player1_character);
                p1_changed = true;

                player1_image.texture = player1_character.character.imaj;
                player1_text.text = player1_character.character.characterName;
            }
            else if (player1_inputs.MovementValues.x < -0.5)
            {
                if (p1_changed) return;
                ArrangeCharacter(-1, player1_character);
                p1_changed = true;

                player1_image.texture = player1_character.character.imaj;
                player1_text.text = player1_character.character.characterName;
            }
            else
            {
                p1_changed = false;
            }
        }
        if (player2_inputs != null)
        {
            if (player2_inputs.MovementValues.x > 0.5)
            {
                if (p2_changed) return;
                ArrangeCharacter(1, player2_character);
                p2_changed = true;

                player2_image.texture = player2_character.character.imaj;
                player2_text.text = player2_character.character.characterName;
            }
            else if (player2_inputs.MovementValues.x < -0.5)
            {
                if (p2_changed) return;
                ArrangeCharacter(-1, player2_character);
                p2_changed = true;

                player2_image.texture = player2_character.character.imaj;
                player2_text.text = player2_character.character.characterName;
            }
            else
            {
                p2_changed = false;
            }
        }
    }

    private void ArrangeCharacter(int change, Player_Character playerCharacter)
    {
        Debug.Log(playerCharacter.character);

        int playerCharacterIndex = 0;
        int allPlayersCount = playerManager.all_Characters.Count;


        for (int i = 0; i < allPlayersCount; i++)
        {
            if (playerManager.all_Characters[i] == playerCharacter)
            {
                playerCharacterIndex = i;
            }
        }

        playerCharacterIndex += change;

        if (playerCharacterIndex < 0)
        {
            playerCharacterIndex = allPlayersCount - 1;
        }
        else if (playerCharacterIndex >= allPlayersCount)
        {
            playerCharacterIndex = 0;
        }

        playerCharacter.character = playerManager.all_Characters[playerCharacterIndex];

        Debug.Log(playerCharacter.character);

        Debug.Log("Ýþlem tamamlandý.");
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
            player1_character = player1.GetComponent<Player_Character>();
            player1_character.character = playerManager.all_Characters[0];
            player1_inputs = player1.GetComponent<Player_Inputs>();
        }
        if(players > 1)
        {
            Player2_Waiting.SetActive(false);
            Player2_Arrived.SetActive(true);
            player2 = GameObject.Find("Player 2");
            player2_character = player2.GetComponent<Player_Character>();
            player2_character.character = playerManager.all_Characters[1];
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
