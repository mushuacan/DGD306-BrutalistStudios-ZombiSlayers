using UnityEngine;
using DG.Tweening;

public class Envoriment_Movement : MonoBehaviour
{
    [Header("Referanslar")]
    [Tooltip("B�l�m sonunda ekran�n ortas�nda duracak olan obje")]
    public GameObject finishLine;
    public GameObject player1;
    public GameObject player2;
    public ZombiAtTheBack_Manager zombiManager;

    [Header("Testerl�k i�in gerekenler")]
    [SerializeField] private KeyCode stopButton;
    [SerializeField] private bool isStopable;
    private bool isMoving;

    [Header("De�i�ken")]
    public float envorimentMovementSpeed;

    [Header("Gizli de�erler")]
    [SerializeField] private float transitionDuration;
    [SerializeField] private float FinishLinePosition;
    [SerializeField] private float endDuration;
    [SerializeField] private float zombiStopDuration;
    [SerializeField] private float zombiStopPosition;
    private bool sessionEnded;
    private bool zombiSessionEnded;


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

        if (finishLine == null || player1 == null || player2 == null || zombiManager == null)
        {
            Debug.LogError("Kayan Obje'de g�sterimsiz referanslar var.");
            Time.timeScale = 0f;
        } 
    }

    // Update is called once per frame
    void Update()
    {
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

        if (finishLine.transform.position.x < FinishLinePosition)
        {
            player1.GetComponent<Player_Movement>().EndGame(envorimentMovementSpeed, transitionDuration, endDuration);
            player2.GetComponent<Player_Movement>().EndGame(envorimentMovementSpeed, transitionDuration, endDuration);
            float newXPosition = transform.position.x - (envorimentMovementSpeed * transitionDuration * 0.5f);
            transform.DOMoveX(newXPosition, transitionDuration).SetEase(Ease.OutQuad);
            sessionEnded = true;
        }
        else
        {
            if (isMoving)
                transform.position = new Vector3(transform.position.x - (envorimentMovementSpeed * Time.deltaTime), transform.position.y, 5);
        }
    }
}
