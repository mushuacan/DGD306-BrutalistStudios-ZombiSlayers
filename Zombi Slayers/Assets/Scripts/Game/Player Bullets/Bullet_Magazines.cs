using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Magazines : MonoBehaviour
{
    [SerializeField] private int magazineBulletCount;
    [SerializeField] private MagazineTypes magazineType;
    private enum MagazineTypes
    {
        Dynamite,
        Shotgun
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Attack player_Attack = collision.GetComponent<Player_Attack>();
            if (player_Attack != null)
            {
                if (magazineType == MagazineTypes.Dynamite && collision.GetComponent<Player_Character>().character.characterName == "Woods")
                {
                    player_Attack.TakeDynamite();
                    Destroy(this.gameObject);
                }
                if (magazineType == MagazineTypes.Shotgun && collision.GetComponent<Player_Character>().character.characterName == "Fletcher")
                {
                    player_Attack.TakeMagazine(magazineBulletCount);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
