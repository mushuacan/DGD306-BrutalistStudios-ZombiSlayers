using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections.Generic;

public class PlayerInputAssigner : MonoBehaviour
{
    public PlayerInput player1;
    public PlayerInput player2;

    private void Awake()
    {
    }

    private void Start()
    {
        InputUser.listenForUnpairedDeviceActivity(false); // doðru kullaným buysa

        List<InputDevice> inputDevices = new List<InputDevice>();

        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad || device is Joystick)
            {
                inputDevices.Add(device);
            }
        }

        if (inputDevices.Count < 2)
        {
            Debug.LogError("Yeterli cihaz bulunamadý! Ýki farklý input device baðlý olmalý.");
            return;
        }

        // Ýki cihazý iki playera eþliyoruz
        InputUser.PerformPairingWithDevice(inputDevices[0], player1.user);
        player1.user.ActivateControlScheme("COTFAM - Player 1"); // Veya inputactions dosyandaki doðru control scheme ismi
        player1.user.AssociateActionsWithUser(player1.actions);

        InputUser.PerformPairingWithDevice(inputDevices[1], player2.user);
        player2.user.ActivateControlScheme("COTFAM - Player 1");
        player2.user.AssociateActionsWithUser(player2.actions);

        foreach (var device in inputDevices)
        {
            Debug.Log("Bulunan cihaz: " + device.displayName);
        }
        Debug.Log("Cihazlar baþarýyla oyunculara atandý.");

    }
}
