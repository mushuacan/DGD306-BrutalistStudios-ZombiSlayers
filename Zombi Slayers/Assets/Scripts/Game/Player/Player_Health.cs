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
    [SerializeField] public bool isSliding;

    private Sequence flashSequence;

    public void StarterPack()
    {
        ArrangeHealth(player.character.health, true);
        undamageableDelay = Time.timeSinceLevelLoad + undamageableDuration;
        Flasher(Color.grey);
    }
    
    public void GiveDamage(bool slideBreaker = false)
    {
        if (health <= 0) return;
        if (player_movement.state == Player_Movement.StateOC.EndGame) return;
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
                Flasher(Color.red); // kýrmýzý yandýrýp söndür
            }
        }
    }

    public void TakeMedKit()
    {
        if (health >= 1)
        {
            player_movement.player_sounder.PlayHealSound();
            ArrangeHealth(+1);
            Flasher(Color.green); // yeþil yanýp söndür
        }
    }
    // Bu metot chat gpt kullanýlarak yapýlmýþtýr.
    private void Flasher(Color color)
    {
        if (flashSequence != null && flashSequence.IsActive())
        {
            flashSequence.Kill(); 
        }

        flashSequence = DOTween.Sequence();

        Color originalColor = Color.white;
        Color flashColor = color;

        for (int i = 0; i < 4; i++)
        {
            flashSequence.Append(spriteRenderer.DOColor(flashColor, undamageableDuration / 8));
            flashSequence.Append(spriteRenderer.DOColor(originalColor, undamageableDuration / 8));
        }
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
            player_UI.ArrangeHearts(health, false);
        }
        else
        {
            health += value;
            player_UI.ArrangeHearts(health, true);
        }
    }

    public void Sliding(float time)
    {
        isSliding = true;
        player_movement.player_animation.SecondAbility(true);
        DOVirtual.DelayedCall(player.character.secondAbilityTimer, () => { isSliding = false;
            player_movement.player_animation.SecondAbility(false);
        });
    }

    public void KillYourself()
    {
        health = 0;
        player_movement.player_sounder.PlayDeathSound();
        this.gameObject.GetComponent<Player_Movement>().Die();
        DieEffect();
    }
}
