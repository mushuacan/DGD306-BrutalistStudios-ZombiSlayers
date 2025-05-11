using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player_Character player;
    [SerializeField] private Animator animationer;
    [SerializeField] private AnimatorController WoodsAnimator;
    [SerializeField] private AnimatorController FletcherAnimator;
    private bool debugger = true;

    public void StarterPack()
    {
        if (player.character.spriter != null)
            _spriteRenderer.sprite = player.character.spriter;
        //CheckCharacter();
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
    public void Walk()
    {
        if (debugger) { Debug.Log("Walk"); }
        animationer.SetBool("IsRunning", false);
    }
    public void Run()
    {
        if (debugger) { Debug.Log("Run"); }
        animationer.SetBool("IsRunning", true);
    }
    public void Death()
    {
        animationer.Play("Death", 0, 0f);

        if (debugger) { Debug.Log("Death"); }
    }
    public void SecondAbility()
    {
        if (debugger) { Debug.Log("ThrowDynamite"); }
        animationer.Play("ThrowDynamite", 0, 0f);
    }
    public void Attack()
    {
        if (debugger) { Debug.Log("Attack"); }
        animationer.Play("Attack", 0, 0f);
    }
    public void Jump()
    {
        if (debugger) { Debug.Log("Jump"); }
        animationer.Play("Jump", 0, 0f);
    }
    public void Jump_1()
    {
        if (debugger) { Debug.Log("Jump_1"); }
        animationer.Play("Jump_1", 0, 0f);
    }
    public void Jump_2()
    {
        if (debugger) { Debug.Log("Jump_2"); }
        animationer.Play("Jump_2", 0, 0f);
    }
}
