using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    public bool follow;
    public float xOffset = 5f; // oyuncunun ne kadar �n�nde olsun
    public float fixedY = 10f; // sabit y konumu
    public float fixedZ = -10f; // sabit z konumu

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Sahnedeki Player_Movement scriptine sahip objeyi bul
        Player_Movement player = FindObjectOfType<Player_Movement>();

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player_Movement scriptine sahip bir obje bulunamad�!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null && follow)
        {
            // Kamera pozisyonunu sadece x'te oyuncuya g�re ayarla
            Vector3 newCameraPos = new Vector3(playerTransform.position.x + xOffset, fixedY, fixedZ);
            transform.position = newCameraPos;
        }
    }
}
