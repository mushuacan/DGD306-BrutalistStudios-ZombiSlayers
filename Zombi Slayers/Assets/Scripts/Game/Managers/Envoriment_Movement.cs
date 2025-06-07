using UnityEngine;
using DG.Tweening;

public class Envoriment_Movement : MonoBehaviour
{
    [Header("Referanslar")]
    [Tooltip("Bölüm sonunda ekranýn ortasýnda duracak olan obje")]
    public GameObject finishLine;
    public GameObject background;
    public ZombiAtTheBack_Manager zombiManager;
    public MiniMapUI MiniMapUI;

    [Header("Testerlýk için gerekenler")]
    [SerializeField] private KeyCode stopButton;
    [SerializeField] private bool isStopable;
    [SerializeField] private bool isStoppedAtFirst;
    private bool isMoving;

    [Header("Deðiþken")]
    public float envorimentMovementSpeed;
    public int playerCount = 2;

    [Header("Gizli deðerler")]
    [SerializeField] private float transitionDuration;
    [SerializeField] private float FinishLinePosition;
    [SerializeField] private float endDuration;
    [SerializeField] private float zombiStopDuration;
    [SerializeField] private float zombiStopPosition;
    public bool sessionEnded;
    private bool zombiSessionEnded;
    private float finishLinePositionAtBeggining;


    void OnValidate()
    {
        transitionDuration = envorimentMovementSpeed * 0.1f * 3f;
        FinishLinePosition = (envorimentMovementSpeed * transitionDuration * 0.5f);
        endDuration = 20 / envorimentMovementSpeed;
        zombiStopDuration = envorimentMovementSpeed * 0.5f + 4;
        zombiStopPosition = (envorimentMovementSpeed * zombiStopDuration * 0.5f) + 8;
    }

    private void Start()
    {
        sessionEnded = false;
        isMoving = true;

        if (finishLine == null || zombiManager == null)
        {
            Debug.LogError("Kayan Obje'de gösterimsiz referanslar var.");
            Time.timeScale = 0f;
        }
        if (isStoppedAtFirst)
        {
            isMoving = false;
        }
        finishLinePositionAtBeggining = finishLine.transform.position.x;
        MiniMapUI = FindAnyObjectByType<MiniMapUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (background != null) background.transform.position = (transform.position * 0.2f);
        if (sessionEnded)
            return;

        if (Input.GetKeyDown(stopButton) && isStopable)
        {
            isMoving = !isMoving;
        }

        if (!zombiSessionEnded)
        {
            if (finishLine.transform.position.x < zombiStopPosition)
            {
                zombiSessionEnded = true;
                zombiManager.EndGame(envorimentMovementSpeed, zombiStopDuration);
            }
        }

        // If Game Ends
        if (finishLine.transform.position.x < FinishLinePosition)
        {
            All_Musician.Instance.PlayMusicVictory();

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                Player_Movement movement = player.GetComponent<Player_Movement>();
                if (movement != null)
                {
                    movement.EndGame(envorimentMovementSpeed, transitionDuration, endDuration);
                }
                else
                {
                    Debug.LogWarning($"Player_Movement component not found on {player.name}");
                }
            }

            float newXPosition = transform.position.x - (envorimentMovementSpeed * transitionDuration * 0.5f);

            background.transform.DOMoveX(newXPosition * 0.2f, transitionDuration).SetEase(Ease.OutQuad);
            transform.DOMoveX(newXPosition, transitionDuration).SetEase(Ease.OutQuad).OnComplete(() => 
            { 
                if ((int)GameSettings.Instance.settings["level"] == FindAnyObjectByType<LevelMaker>().level - 1)
                    GameSettings.Instance.settings["level"] = (int)GameSettings.Instance.settings["level"] + 1;
                InGameMenuManager igmm = FindAnyObjectByType<InGameMenuManager>();
                igmm.OpenEndImage(0f);
                igmm.OpenEndMenu(true);
            });

            if (ScoreManager.Instance != null && ScoreManager.Instance.gameObject.activeInHierarchy)
            {
                ScoreManager.Instance.StopAutoScore();
            }
            sessionEnded = true;
        }
        else
        {
            if (isMoving)
            {
                transform.position = new Vector3(transform.position.x - (envorimentMovementSpeed * Time.deltaTime), transform.position.y, 5);
            }
        }
        if (MiniMapUI != null)
            MiniMapUI.UpdateSlider(finishLine.transform.position.x / finishLinePositionAtBeggining);
    }

    public void AllPlayersDied()
    {
        All_Musician.Instance.PlayMusicGameOver();
        sessionEnded = true;
        InGameMenuManager igmm = FindAnyObjectByType<InGameMenuManager>();
        igmm.OpenEndImage(finishLine.transform.position.x / finishLinePositionAtBeggining);
        transform.DOMoveX(transform.position.x - envorimentMovementSpeed, 2).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            igmm.OpenEndMenu(false);
        });
    }
}
