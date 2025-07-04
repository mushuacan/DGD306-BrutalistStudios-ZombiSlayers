using UnityEngine;
using DG.Tweening;
using System;


public class Player_Health : MonoBehaviour
{
    public event Action OnPlayerDied;

    [Header("Settings")]
    [SerializeField] private float undamageableDuration;
    [Tooltip("0 daha g�r�nmez yapar")]
    [Range(0f, 1f)] [SerializeField] private float undamageableImpulsePower;

    [Header("Referances")]
    [SerializeField] private SpriteRenderer spriteRenderer;  // Karakterin SpriteRenderer'�
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Movement player_movement;
    [SerializeField] private GameObject bloodParticle;


    [Header("(private variables)")]
    [SerializeField] public int health;
    [SerializeField] public float undamageableDelay;
    [SerializeField] public bool isSliding;
    public bool immortalCheat;

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
            if (!immortalCheat)
            ArrangeHealth(-1);
            CreateBloodParticles();

            if (health <= 0)
            {
                player_movement.player_sounder.PlayDeathSound();
                this.gameObject.GetComponent<Player_Movement>().Die();
                DieEffect();
            }
            else
            {
                float difficulty = (float)GameSettings.Instance.settings["difficulty"]; 
                float animationDuration = -0.6f * difficulty + 1.6f;

                undamageableDelay = Time.timeSinceLevelLoad + undamageableDuration * animationDuration;
                Flasher(Color.red, animationDuration); // k�rm�z� yand�r�p s�nd�r
            }
        }
    }

    private void CreateBloodParticles()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
    }

    public void TakeMedKit()
    {
        if (health >= 1)
        {
            player_movement.player_sounder.PlayHealSound();
            ArrangeHealth(+1);
            Flasher(Color.green); // ye�il yan�p s�nd�r
        }
    }
    // Bu metot chat gpt kullan�larak yap�lm��t�r.
    private void Flasher(Color color, float animationDuration = 1f)
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
            flashSequence.Append(spriteRenderer.DOColor(flashColor, undamageableDuration * animationDuration / 8));
            flashSequence.Append(spriteRenderer.DOColor(originalColor, undamageableDuration * animationDuration / 8));
        }
    }

    private void DieEffect()
    {

        OnPlayerDied?.Invoke(); // Event'i �a��r
        //spriteRenderer.DOFade(undamageableImpulsePower, undamageableDuration / 2);
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
