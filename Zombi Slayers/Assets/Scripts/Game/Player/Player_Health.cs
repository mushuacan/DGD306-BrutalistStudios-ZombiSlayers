using UnityEngine;
using DG.Tweening;
using System;


public class Player_Health : MonoBehaviour
{
    public event Action OnPlayerDied;

    [Header("Settings")]
    [SerializeField] private float undamageableDuration;
    [Tooltip("0 daha görünmez yapar")]
    [Range(0f, 1f)] [SerializeField] private float undamageableImpulsePower;

    [Header("Referances")]
    [SerializeField] private SpriteRenderer spriteRenderer;  // Karakterin SpriteRenderer'ý
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Movement player_movement;


    [Header("(private variables)")]
    [SerializeField] public int health;
    [SerializeField] public float undamageableDelay;
    [SerializeField] private bool isSliding;

    public void StarterPack()
    {
        ArrangeHealth(player.character.health, true);
        undamageableDelay = Time.timeSinceLevelLoad + undamageableDuration;
        FlashEffect();
    }
    
    public void GiveDamage(bool slideBreaker = false)
    {
        if (isSliding)
        {
            if (!slideBreaker)
            {
                return;
            }
        }
        if (undamageableDelay < Time.timeSinceLevelLoad)
        {
            ArrangeHealth(-1);

            if (health <= 0)
            {
                player_movement.player_sounder.PlayDeathSound();
                this.gameObject.GetComponent<Player_Movement>().Die();
                DieEffect();
            }
            else
            {
                undamageableDelay = Time.timeSinceLevelLoad + undamageableDuration;
                FlashEffect();
            }
        }
    }

    public void TakeMedKit()
    {
        player_movement.player_sounder.PlayHealSound();
        ArrangeHealth(+1);
    }
    private void FlashEffect()
    {
        // Yanýp sönme efekti için DOTween kullanýyoruz
        // 0.1 saniye boyunca alpha'yý 0'a düþürüp, sonra 0.1 saniyede tekrar 1 yapýyoruz
        // Alpha'yý geri 1 yapýyoruz.
        spriteRenderer.DOFade(undamageableImpulsePower, undamageableDuration/4).OnComplete(() =>
        {
            spriteRenderer.DOFade(1f - undamageableImpulsePower, undamageableDuration / 4).OnComplete(() =>
            {
                spriteRenderer.DOFade(undamageableImpulsePower, undamageableDuration / 4).OnComplete(() =>
                {
                    spriteRenderer.DOFade(1f, undamageableDuration / 4);
                });
            }); 
        });
    }
    private void DieEffect()
    {

        OnPlayerDied?.Invoke(); // Event'i çaðýr
        spriteRenderer.DOFade(undamageableImpulsePower, undamageableDuration / 2);
    }

    private void ArrangeHealth(int value, bool set = false)
    {
        if (set)
        {
            health = value;
        }
        else
        {
            health += value;
        }
        player_UI.ArrangeHearts(health);
    }

    public void Sliding(float time)
    {
        isSliding = true;
        player_movement.player_animation.SecondAbility(true);
        DOVirtual.DelayedCall(player.character.secondAbilityTimer, () => { isSliding = false;
            player_movement.player_animation.SecondAbility(false);
        });
    }
}
