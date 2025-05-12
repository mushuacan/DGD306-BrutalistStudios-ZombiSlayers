using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sounder : MonoBehaviour
{
    [SerializeField] private Player_Character player;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] woodsAttack;
    [SerializeField] private AudioClip[] fletchersAttack;
    [SerializeField] private AudioClip[] fletchersReloads;
    [SerializeField] private AudioClip[] playerDeath;
    [SerializeField] private AudioClip[] playerHurt;
    [SerializeField] private AudioClip[] playerHeal;

    public void PlayAttackSound()
    {
        if (player.character.characterName == "Woods")
        {
            audioSource.PlayOneShot(woodsAttack[Random.Range(0, woodsAttack.Length)]);
        }
        if (player.character.characterName == "Fletcher")
        {
            audioSource.PlayOneShot(fletchersAttack[Random.Range(0, fletchersAttack.Length)]);
        }
    }
    public void PlayFletchersReload()
    {
        audioSource.PlayOneShot(fletchersReloads[Random.Range(0, fletchersReloads.Length)]);
    }
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(playerDeath[Random.Range(0, playerDeath.Length)]);
    }
    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)]);
    }
    public void PlayHealSound()
    {
        audioSource.PlayOneShot(playerHeal[Random.Range(0, playerHeal.Length)]);
    }
}
