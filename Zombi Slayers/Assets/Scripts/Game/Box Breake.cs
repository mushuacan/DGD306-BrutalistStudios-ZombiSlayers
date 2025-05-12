using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreake : MonoBehaviour
{
    public Scriptable_BoxInsiders insider;
    private bool isTriggered = false;

    [SerializeField] private AudioClip[] clips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return; // Eðer zaten tetiklendiyse, iþlemi durdur

        if (collision.CompareTag("Player_Bullet"))
        {
            isTriggered = true;

            if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
            Instantiate(insider.prefab, transform.position, transform.rotation, transform.parent);
            Destroy(this.gameObject);
        }
    }

}
