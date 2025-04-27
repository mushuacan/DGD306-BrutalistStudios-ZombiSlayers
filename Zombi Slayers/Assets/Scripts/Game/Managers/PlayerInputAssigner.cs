using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections.Generic;

public class PlayerInputAssigner : MonoBehaviour
{
    public PlayerInput player1;
    public PlayerInput player2;

    private void Start()
    {
        List<InputDevice> gamepads = new List<InputDevice>();

        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad)
                gamepads.Add(device);
        }

        int assignedPlayers = 0;

        foreach (var gamepad in gamepads)
        {
            if (assignedPlayers == 0)
            {
                InputUser.PerformPairingWithDevice(gamepad, player1.user);
                assignedPlayers++;
            }
            else if (assignedPlayers == 1)
            {
                InputUser.PerformPairingWithDevice(gamepad, player2.user);
                assignedPlayers++;
                break; // ikisi de doldu
            }
        }
    }
}
