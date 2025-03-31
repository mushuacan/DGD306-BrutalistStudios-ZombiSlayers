using UnityEngine;
using DG.Tweening;


public class Player_Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float undamageableDuration;
    [Tooltip("0 daha g�r�nmez yapar")]
    [Range(0f, 1f)] [SerializeField] private float undamageableImpulsePower;

    [Header("Referances")]
    [SerializeField] private SpriteRenderer spriteRenderer;  // Karakterin SpriteRenderer'�
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Character player;


    [Header("(private variables)")]
    [SerializeField] private int health;
    [SerializeField] public float undamageableDelay;
    [SerializeField] private bool isSliding;

    // Start is called before the first frame update
    void Start()
    {
        ArrangeHealth(player.character.health, true);
        undamageableDelay = Time.timeSinceLevelLoad;
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
        ArrangeHealth(+1);
    }
    private void FlashEffect()
    {
        // Yan�p s�nme efekti i�in DOTween kullan�yoruz
        // 0.1 saniye boyunca alpha'y� 0'a d���r�p, sonra 0.1 saniyede tekrar 1 yap�yoruz
        // Alpha'y� geri 1 yap�yoruz.
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
        DOVirtual.DelayedCall(player.character.slideTimer, () => { isSliding = false; });
    }
}
