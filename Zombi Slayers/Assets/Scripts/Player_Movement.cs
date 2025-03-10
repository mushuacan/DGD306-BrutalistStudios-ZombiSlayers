using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Player_Movement : MonoBehaviour
{
    public KeyCode moveUp_Button;
    public KeyCode moveDown_Button;
    public KeyCode moveLeft_Button;
    public KeyCode moveRight_Button;
    public KeyCode attack_Button;
    public KeyCode dodge_Button;

    public float movementSpeed;
    public float xLeftEdge;
    public float xRightEdge;

    public float animationSpeed;
    public float jumpAnimationDuration;
    public float jumpAnimationUpDistance;
    public int lane;
    public float[] laneYPositions;

    public StateOC state;


    public enum StateOC // State of Character
    {
        Running,
        Jumping,
        Attacking,
        Dodgeing
    }


    // Start is called before the first frame update
    void Start()
    {
        state = StateOC.Running;
        lane = 1;
        transform.position = new Vector2(transform.position.x, laneYPositions[lane]);
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
            if (transform.position.x < xRightEdge)
                transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(moveLeft_Button) && (state == StateOC.Running || state == StateOC.Jumping))
        {
            if (transform.position.x > xLeftEdge)
                transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
        }
    }

    public void JumpBetweenLanes(string Vertical)
    {
        if (Vertical == "Down")
        {
            transform.DOMoveY(jumpAnimationUpDistance + transform.position.y, (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // Yukarý hareket tamamlandýðýnda, asýl hedef Y pozisyonuna doðru hareket et
                transform.DOMoveY(laneYPositions[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    state = StateOC.Running;
                });
            });
        }
        if (Vertical == "Up")
        {
            transform.DOMoveY(laneYPositions[lane], (jumpAnimationDuration /
                animationSpeed) * 0.8f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                state = StateOC.Running;
            });
        }
    }
}
