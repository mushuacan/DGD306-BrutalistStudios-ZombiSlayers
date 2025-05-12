using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Movement player_movement;
    [SerializeField] private Animator animationer;
    [SerializeField] private RuntimeAnimatorController WoodsAnimator;
    [SerializeField] private RuntimeAnimatorController FletcherAnimator;
    private bool debugger = false;

    public void StarterPack()
    {
        if (player.character.spriter != null)
        {
            _spriteRenderer.sprite = player.character.spriter;
        }
        else
        {
            Debug.LogError("Spriter gözükmüyor");
        }
        if (player_movement.animations) CheckCharacter();
    }
    private void CheckCharacter()
    {
        if (player.character.characterName == "Fletcher")
        {
            animationer.runtimeAnimatorController = FletcherAnimator;
        }
        else if (player.character.characterName == "Woods")
        {
            animationer.runtimeAnimatorController = WoodsAnimator;
        }
    }
    private bool CheckIfControllerNull()
    {
        if (animationer.runtimeAnimatorController == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Walk()
    {
        if (CheckIfControllerNull()) return;

        if (debugger) { Debug.Log("Walk"); }
        animationer.SetBool("IsRunning", false);
    }
    public void Run()
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("Run"); }
        animationer.SetBool("IsRunning", true);
    }
    public void Death()
    {
        if (CheckIfControllerNull()) return;
        animationer.Play("Death", 0, 0f);

        if (debugger) { Debug.Log("Death"); }
    }
    public void SecondAbility(bool boolean = false)
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("ThrowDynamite"); }
        if(player.character.characterName == "Fletcher")
        {
            animationer.SetBool("IsSliding", boolean);
        }
        else if (player.character.characterName == "Woods")
        {
            animationer.Play("ThrowDynamite", 0, 0f);
        }
    }
    public void Attack()
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("Attack"); }
        animationer.Play("Attack", 0, 0f);
    }
    public void Jump()
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("Jump"); }
        animationer.Play("Jump", 0, 0f);
    }
    public void Jump_1()
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("Jump_1"); }
        animationer.Play("Jump_1", 0, 0f);
    }
    public void Jump_2()
    {
        if (CheckIfControllerNull()) return;
        if (debugger) { Debug.Log("Jump_2"); }
        animationer.Play("Jump_2", 0, 0f);
    }
}
