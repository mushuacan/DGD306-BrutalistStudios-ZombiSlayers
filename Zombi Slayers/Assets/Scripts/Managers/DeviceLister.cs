using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceLister : MonoBehaviour
{
    void Start()
    {
        // Input System cihazlar�n� listele
        var devices = InputSystem.devices;

        // E�er cihaz ba�l�ysa, her birini konsola yazd�r
        if (devices.Count > 0)
        {
            foreach (var device in devices)
            {
                Debug.Log("Ba�l� cihaz: " + device.displayName);
            }
        }
        else
        {
            Debug.Log("Ba�l� cihaz yok.");
        }
    }
}
