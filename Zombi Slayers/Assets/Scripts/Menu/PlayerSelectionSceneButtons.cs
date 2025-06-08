using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerSelectionSceneButtons : MonoBehaviour
{
    [Header("Deðiþkenler")]
    public int playerCount;
    public string gameType;
    public float secondsToReady;

    [Header("Menüler")]
    public GameObject menu1;
    public GameObject menu2;

    [Header("Referans-ý Menüsel")]
    public GameObject PlayerWithArrowKeys_Prefab;
    public GameObject Player1_Waiting;
    public GameObject Player1_Arrived;
    public GameObject Player2_Waiting;
    public GameObject Player2_Arrived;
    public GameObject Menu1_SelectedButton;
    public GameObject Menu2_SelectedButton;


    [Header("Referans-ý Prefab")]
    public GameObject PlayerInputManagerPrefab;
    public PlayerManager playerManager;

    [Header("Referans-ý player")]
    public GameObject player1;
    public GameObject player2;
    public Player_Inputs player1_inputs, player2_inputs;
    public Player_Character player1_character, player2_character;
    private float player1_timer, player2_timer, outTimer;

    [Header("Referans-ý Menüsel Faktörs")]
    public RawImage player1_image;
    public RawImage player2_image;
    public TextMeshProUGUI player1_text, player2_text, p1ReadyText, p2ReadyText, p1infoText, p2infoText ,outText;
    private string player1_device, player2_device;

    [Header("Booleanlar")]
    private bool p1_changed, p2_changed, p1ready, p2ready, p1gettingReady, p2gettingReady, outTimerStarted, createdPWithArrowKeys;
    [Header("Tuþ Girdileri")]
    private KeyCode[] buttons; // buttonsForSecondKeyboardPlayer


    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        EventSystem.current.SetSelectedGameObject(Menu1_SelectedButton);

        //playerManager = FindAnyObjectByType<PlayerManager>();

        Player1_Waiting.SetActive(true);
        Player1_Arrived.SetActive(false);
        Player2_Waiting.SetActive(true);
        Player2_Arrived.SetActive(false);

        p1_changed = false;
        p2_changed = false;
        createdPWithArrowKeys = false;
    }

    public void Button_LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        if (player1_inputs != null)
        {
            ArrangePlayer1Selection();
            ArrangePlayer1Ready();
            CheckIfPlayerWithArrowJoins();
        }
        if (player2_inputs != null)
        {
            ArrangePlayer2Selection();
            ArrangePlayer2Ready();
        }
        CheckIfPlayersReady();
        CheckIfPlayerWantsOut();
    }

    private void CheckIfPlayerWantsOut()
    {
        if (CheckPlayerOutInputs())
        {
            if (outTimerStarted)
            {
                outText.text = "Getting Out in " + (outTimer - Time.timeSinceLevelLoad).ToString("F1");
                if (outTimer < Time.timeSinceLevelLoad)
                {
                    Button_LoadScene("Menu Scene");
                }
            }
            else
            {
                outTimerStarted = true;
            }
        }
        else
        {
            outText.text = "";
            outTimer = Time.timeSinceLevelLoad + 1;
            outTimerStarted = false;
        }
    }
    private bool CheckPlayerOutInputs()
    {
        if (player1_inputs != null)
        {
            if (player1_inputs.button1pressed)
            {
                return true;
            }
        }
        if (player2_inputs != null)
        {
            if (player2_inputs.button1pressed)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckIfPlayersReady()
    {
        if ((p1ready && playerCount == 1) || (p1ready && p2ready && playerCount == 2))
        {
            if (gameType == "EndlessRun")
            {
                Button_LoadScene("EndlessRunner");
            }
            else
            {
                Button_LoadScene("Level Menu");
            }
        }
    }
    private void ArrangePlayer1Ready()
    {
        if (p1ready)
        {
            if (player1_inputs.button1pressed)
            {
                p1ready = false;
            }
        }
        else
        {
            if (player1_inputs.button0pressed)
            {
                if (!p1gettingReady)
                {
                    player1_timer = Time.timeSinceLevelLoad + secondsToReady;
                }
                if (player1_timer < Time.timeSinceLevelLoad)
                {
                    p1ready = true;
                    p1ReadyText.text = "Ready";
                    return;
                }
                p1gettingReady = true;
                p1ReadyText.text = "Getting Ready in " + (player1_timer - Time.timeSinceLevelLoad).ToString("F1");
            }
            else
            {
                p1ready = false;
                p1gettingReady = false;
                p1ReadyText.text = "Not Ready";
            }
        }
    }
    private void ArrangePlayer2Ready()
    {
        if (p2ready)
        {
            if (player2_inputs.button1pressed)
            {
                p2ready = false;
            }
        }
        else
        {
            if (player2_inputs.button0pressed)
            {
                if (!p2gettingReady)
                {
                    player2_timer = Time.timeSinceLevelLoad + secondsToReady;
                }
                if (player2_timer < Time.timeSinceLevelLoad)
                {
                    p2ready = true;
                    p2ReadyText.text = "Ready";
                    return;
                }
                p2gettingReady = true;
                p2ReadyText.text = "Getting Ready in " + (player2_timer - Time.timeSinceLevelLoad).ToString("F1");
            }
            else
            {
                p2ready = false;
                p2gettingReady = false;
                p2ReadyText.text = "Not Ready";
            }
        }
    }
    private void ArrangePlayer1Selection()
    {
        if (p1ready) return;
        if (player1_inputs.MovementValues.x > 0.5)
        {
            CharacterArranger(1, 1);
        }
        else if (player1_inputs.MovementValues.x < -0.5)
        {
            CharacterArranger(1, -1);
        }
        else
        {
            p1_changed = false;
        }
    }
    private void ArrangePlayer2Selection()
    {
        if (p2ready) return;
        if (player2_inputs.MovementValues.x > 0.5)
        {
            CharacterArranger(2, 1);
        }
        else if (player2_inputs.MovementValues.x < -0.5)
        {
            CharacterArranger(2, -1);
        }
        else
        {
            p2_changed = false;
        }
    }

    private void CharacterArranger(int p, int change)
    {
        if (p == 1)
        {
            if (p1_changed) return;

            ArrangeCharacter(change, player1_character);
            p1_changed = true;

            player1_image.texture = player1_character.character.imaj;
            player1_text.text = player1_character.character.characterName;
        }
        else if (p == 2)
        {
            if (p2_changed) return;

            ArrangeCharacter(change, player2_character);
            p2_changed = true;

            player2_image.texture = player2_character.character.imaj;
            player2_text.text = player2_character.character.characterName;
        }
    }

    private void ArrangeCharacter(int change, Player_Character playerCharacter)
    {
        Debug.Log(playerCharacter.character);

        int playerCharacterIndex = 0;
        int allPlayersCount = playerManager.all_Characters.Count;


        for (int i = 0; i < allPlayersCount; i++)
        {
            if (playerManager.all_Characters[i] == playerCharacter.character)
            {
                playerCharacterIndex = i;
                break;
            }
        }

        // Deðiþikliði uygula ve dizinin sýnýrlarý dýþýna çýkýldýðýnda sar
        playerCharacterIndex = (playerCharacterIndex + change + allPlayersCount) % allPlayersCount;

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

    public void Versus()
    {
        playerCount = 1;
        gameType = "Versus";
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
            
            if (player1_inputs.isItOnlyKeyboard)
            {
                player1_device = "Keyboard With Arrow Keys";
            }
            else
            {
                player1_device = player1_inputs.playerInput.devices[0].displayName;
                Debug.Log("Player 1 input device: " + player1_device);
            }

            ArrangePlayerUIofHintButtons(p1infoText, player1_device);
        }
        if (players > 1)
        {
            Player2_Waiting.SetActive(false);
            Player2_Arrived.SetActive(true);
            player2 = GameObject.Find("Player 2");
            player2_character = player2.GetComponent<Player_Character>();
            player2_character.character = playerManager.all_Characters[0];
            player2_inputs = player2.GetComponent<Player_Inputs>();

            if (player2_inputs.isItOnlyKeyboard)
            {
                player2_device = "Keyboard With Arrow Keys";
                Debug.Log("Player 2 input device: " + player2_device);
            }
            else
            {
                player2_device = player2_inputs.playerInput.devices[0].displayName;
                Debug.Log("Player 2 input device: " + player2_device);
            }
            ArrangePlayerUIofHintButtons(p2infoText, player2_device);
        }
    }

    private void ArrangePlayerUIofHintButtons(TextMeshProUGUI textbox, string device)
    {
        string texter = "";
        if (device == "Keyboard")
        {
            texter = "V for ready\r\nB for out\r\nA D for choosing character";
        }
        else if (device == "Keyboard With Arrow Keys")
        {
            texter = "1 for ready\r\n2 for out\r\n<- -> for choosing character";
        }
        else
        {
            texter = "A for ready\r\nB for out\r\n<- -> for choosing character";
        }
        textbox.text = texter;
    }

    private void SwitchMenus()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Menu2_SelectedButton);
        GameObject PlayerDetecter = Instantiate(PlayerInputManagerPrefab);
    }
    public void BackToMenu1()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        EventSystem.current.SetSelectedGameObject(Menu1_SelectedButton);
    }
    public void OpenCharacterSelect()
    {
        menu2.SetActive(true);
        menu1.SetActive(false);
    }
    public void CreateDeviceDetecter()
    {
        Instantiate(PlayerInputManagerPrefab);
    }
    public void CreatePlayerWithKeyboard()
    {
        GameObject createdPlayer = Instantiate(PlayerWithArrowKeys_Prefab);
        PlayerInput player = createdPlayer.GetComponent<PlayerInput>();
        playerManager.OnPlayerJoined();
    }
    private void CheckIfPlayerWithArrowJoins()
    {
        if (gameType != "Multiplayer") return;

        if (createdPWithArrowKeys) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            CreatePlayerWithKeyboard();
            createdPWithArrowKeys = true;
        }
    }
}
