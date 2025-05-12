using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sounder : MonoBehaviour
{
    [SerializeField] private Player_Character player;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip woodsAttack;
    [SerializeField] private AudioClip fletchersAttack;

    public void PlayAttackSound()
    {
        if (player.character.characterName == "Woods")
        {
            audioSource.PlayOneShot(woodsAttack);
        }
        if (player.character.characterName == "Fletcher")
        {
            audioSource.PlayOneShot(fletchersAttack);
        }
    }
}
