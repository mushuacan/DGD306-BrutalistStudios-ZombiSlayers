using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Inputs : MonoBehaviour
{
    public bool button0pressed;
    public bool button1pressed;
    public bool button2pressed;
    public bool button3pressed;
    public bool rightShoulderPressed;
    public bool leftShoulderPressed;
    public bool startPressed;
    public bool sellectPressed;
    public Vector2 MovementValues;
    public PlayerInput playerInput;

    public bool isItOnlyKeyboard;
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;
    public KeyCode AttackKey;
    public KeyCode SecondKey;
    public KeyCode StartKey;
    public KeyCode SelectKey;
    private Vector2 MovementVector2;

    private void Update()
    {
        if (isItOnlyKeyboard)
        {
            if (Input.GetKey(UpKey))
            {
                MovementVector2.y = 1;
            }
            else if (Input.GetKey(DownKey))
            {
                MovementVector2.y = -1;
            }else
            {
                MovementVector2.y = 0;
            }
            if (Input.GetKey(LeftKey))
            {
                MovementVector2.x = -1;
            }
            else if (Input.GetKey(RightKey))
            {
                MovementVector2.x = 1;
            }
            else
            {
                MovementVector2.x = 0;
            }

            MovementValues = MovementVector2;

            if (Input.GetKey(AttackKey))
            {
                button0pressed = true;
            }
            else
            {
                button0pressed = false;
            }

            if (Input.GetKey(SecondKey))
            {
                button1pressed = true;
            }
            else
            {
                button1pressed = false;
            }


            if (Input.GetKey(StartKey))
            {
                startPressed = true;
            }
            else
            {
                startPressed = false;
            }

            if (Input.GetKey(SelectKey))
            {
                sellectPressed = true;
            }
            else
            {
                sellectPressed = false;
            }

        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValues = context.ReadValue<Vector2>();
        //Debug.Log("movement:" + MovementValues);
    }
    public void OnButton0(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            button0pressed = true;
            //Debug.Log("OnButton0 pressed");
        }

        if (context.canceled)
        {
            button0pressed = false;
        }
    }
    public void OnButton1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            button1pressed = true;
            //Debug.Log("OnButton1 pressed");
        }

        if (context.canceled)
        {
            button1pressed = false;
        }
    }
    public void OnButton2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            button2pressed = true;
            //Debug.Log("OnButton2 pressed");
        }

        if (context.canceled)
        {
            button2pressed = false;
        }
    }
    public void OnButton3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            button3pressed = true;
            //Debug.Log("OnButton3 pressed");
        }

        if (context.canceled)
        {
            button3pressed = false;
        }
    }
    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            startPressed = true;
        }

        if (context.canceled)
        {
            startPressed = false;
        }
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            sellectPressed = true;
        }

        if (context.canceled)
        {
            sellectPressed = false;
        }
    }
    public void OnLeftShoulder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            leftShoulderPressed = true;
        }

        if (context.canceled)
        {
            leftShoulderPressed = false;
        }
    }

    public void OnRightShoulder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightShoulderPressed = true;
        }

        if (context.canceled)
        {
            rightShoulderPressed = false;
        }
    }
}
