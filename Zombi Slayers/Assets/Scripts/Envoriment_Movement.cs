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

    [Header("Deðiþken")]
    public float envorimentMovementSpeed;

    [Header("Gizli deðerler")]
    [SerializeField] private float transitionDuration;
    [SerializeField] private float endDuration;
    [SerializeField] private float FinishLinePosition;
    private bool sessionEnded;


    void OnValidate()
    {
        transitionDuration = envorimentMovementSpeed * 0.1f * 3f;
        FinishLinePosition = (envorimentMovementSpeed * transitionDuration * 0.5f);
        endDuration = 20 / envorimentMovementSpeed;
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
