using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envoriment_Movement : MonoBehaviour
{
    [Header("Referanslar")]
    [Tooltip("Bölüm sonunda ekranýn ortasýnda duracak olan obje")]
    public GameObject finishLine;
    public GameObject player1;
    public GameObject player2;
    public ZombiAtTheBack_Manager zombiManager;

    [Header("Deðiþken")]
    public float envorimentMovementSpeed;

    [Header("Gizli deðerler")]
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
    }

    // Update is called once per frame
    void Update()
    {
        if (sessionEnded)
            return;

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
            transform.position = new Vector3(transform.position.x - (envorimentMovementSpeed * Time.deltaTime), transform.position.y, 5);
        }
    }
}
