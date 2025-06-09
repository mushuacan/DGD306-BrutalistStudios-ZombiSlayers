using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreake : MonoBehaviour
{
    public Scriptable_BoxInsiders insider;
    private bool isTriggered = false;

    [SerializeField] private LaneFinder laneFinder;
    [SerializeField] private AudioClip[] clips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return; // Eðer zaten tetiklendiyse, iþlemi durdur

        if (collision.CompareTag("Player_Bullet"))
        {
            isTriggered = true;

            if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips);

            Vector3 pozisyon = new Vector3(transform.position.x, LaneFinder.laneYPositions[laneFinder.lane], transform.position.z);

            if (insider != null)
            Instantiate(insider.prefab, pozisyon, transform.rotation, transform.parent);
            Destroy(this.gameObject);
        }
    }

}
