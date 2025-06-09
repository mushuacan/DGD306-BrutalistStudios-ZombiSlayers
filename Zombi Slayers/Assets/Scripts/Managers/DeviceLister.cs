using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceLister : MonoBehaviour
{
    void Start()
    {
        // Input System cihazlarýný listele
        var devices = InputSystem.devices;

        // Eðer cihaz baðlýysa, her birini konsola yazdýr
        if (devices.Count > 0)
        {
            foreach (var device in devices)
            {
                Debug.Log("Baðlý cihaz: " + device.displayName);
            }
        }
        else
        {
            Debug.Log("Baðlý cihaz yok.");
        }
    }
}
