using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Player_Movement : MonoBehaviour
{
    [Header("BUTTONS")]
    [SerializeField] private KeyCode moveUp_Button;
    [SerializeField] private KeyCode moveDown_Button;
    [SerializeField] private KeyCode moveRight_Button;
    [SerializeField] private KeyCode moveLeft_Button;
    [SerializeField] private KeyCode attack_Button;
    [SerializeField] private KeyCode dodge_Button;

    [Header("Playground Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;

    [Header("Animation Settings")]
    [SerializeField] private float animationSpeed;
    [SerializeField] private float jumpAnimationDuration;
    [SerializeField] private float jumpAnimationUpDistance;

    [Header("Starting")]
    [SerializeField] [Range(-8, 5)] private int startPositionX;
    [SerializeField] [Range(1, 3)] private int lane;
    private float[] laneYPositions = { -1f, -3f, 0.25f, 3.5f };

    [Header("(private variables)")]
    [SerializeField] private StateOC state;


    public enum StateOC // State of Character
    {
        Running,
        Jumping,
        Attacking,
        Dodgeing,
        EndGame
    }


    // Start is called before the first frame update
    void Start()
    {
        state = StateOC.Running;
        //lane = 2;
        transform.position = new Vector2(startPositionX, laneYPositions[lane]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveUp_Button) && state == StateOC.Running)
        {
            if (lane == 3)
            {
                //yapamaz
            }
            else
            {
                state = StateOC.Jumping;
                lane = lane + 1;
                JumpBetweenLanes("Up");
            }
        }
        if (Input.GetKey(moveDown_Button) && state == StateOC.Running)
        {
            if (lane == 1)
            {
                //yapamaz
            }
            else
            {
                state = StateOC.Jumping;
                lane = lane - 1;
                JumpBetweenLanes("Down");
            }
        }
        if (Input.GetKeyDown(attack_Button) && state == StateOC.Running)
        {
            state = StateOC.Attacking;
        }
        if (Input.GetKey(moveRight_Button) && (state == StateOC.Running || state == StateOC.Jumping))
        {
            if (transform.position.x < rightBoundary)
                transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(moveLeft_Button) && (state == StateOC.Running || state == StateOC.Jumping))
        {
            if (transform.position.x > leftBoundary)
                transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
        }
    }

    public void JumpBetweenLanes(string Vertical)
    {
        if (Vertical == "Down")
        {
            transform.DOMoveY(jumpAnimationUpDistance + transform.position.y, (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // Yukar� hareket tamamland���nda, as�l hedef Y pozisyonuna do�ru hareket et
                transform.DOMoveY(laneYPositions[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    if (state != StateOC.EndGame)
                        state = StateOC.Running;
                });
            });
        }
        if (Vertical == "Up")
        {
            transform.DOMoveY(jumpAnimationUpDistance + laneYPositions[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // Yukar� hareket tamamland���nda, as�l hedef Y pozisyonuna do�ru hareket et
                transform.DOMoveY(laneYPositions[lane], (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    if (state != StateOC.EndGame)
                        state = StateOC.Running;
                });
            });
        }
    }

    public void EndGame(float movementSpeed, float transitionDuration, float endDuration)
    {
        state = StateOC.EndGame;
        float xPosition = transform.position.x + (movementSpeed/2) * transitionDuration;
        transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            transform.DOMoveX(transform.position.x + movementSpeed * endDuration, endDuration).SetEase(Ease.Linear);
        });
    }
}
