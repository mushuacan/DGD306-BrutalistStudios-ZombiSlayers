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
        List<InputDevice> inputDevices = new List<InputDevice>();

        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad || device is Joystick)
            {
                inputDevices.Add(device);
            }
        }

        int assignedPlayers = 0;

        foreach (var device in inputDevices)
        {
            if (assignedPlayers == 0)
            {
                InputUser.PerformPairingWithDevice(device, player1.user);
                player1.user.ActivateControlScheme("Gamepad"); // veya "Joystick" - scheme ismine göre
                player1.user.AssociateActionsWithUser(player1.actions); // bu önemli
                assignedPlayers++;
            }
            else if (assignedPlayers == 1)
            {
                InputUser.PerformPairingWithDevice(device, player2.user);
                player2.user.ActivateControlScheme("Gamepad");
                player2.user.AssociateActionsWithUser(player2.actions);
                assignedPlayers++;
                break;
            }
        }
    }
}
