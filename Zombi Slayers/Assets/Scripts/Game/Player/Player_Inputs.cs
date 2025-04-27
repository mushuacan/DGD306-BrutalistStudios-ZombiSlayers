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
    public void OnMovement(InputValue value)
    {
        MovementValues = value.Get<Vector2>();
        Debug.Log("movement:" + MovementValues);
    }
    public void OnButton0(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            button0pressed = true;
            Debug.Log("OnButton0 pressed");
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
            Debug.Log("OnButton1 pressed");
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
            Debug.Log("OnButton2 pressed");
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
            Debug.Log("OnButton3 pressed");
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

    //void OnRightShoulder(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        Debug.Log("Button0 basýlmaya baþlandý");
    //    }

    //    if (context.performed)
    //    {
    //        Debug.Log("Button0 tamamlandý (tek basma gibi düþünebilirsin)");
    //    }

    //    if (context.canceled)
    //    {
    //        Debug.Log("Button0 býrakýldý");
    //    }
    //}
}
