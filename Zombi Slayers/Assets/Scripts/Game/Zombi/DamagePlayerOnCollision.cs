using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.UI;

public class DamagePlayerOnCollision : MonoBehaviour
{
    [SerializeField] private bool destroyOnCollision;
    [Tooltip("For only bullets")]
    [SerializeField] private bool canSlideable;
    [SerializeField] private animate animater;
    [SerializeField] private Animator animationer;
    [SerializeField] private AudioClip[] clipsForDamagingPlayer;
    [SerializeField] private AudioClip[] clipsForDodgedPlayer;

    private enum animate {
        none,
        zombi
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Movement pMove = collision.GetComponent<Player_Movement>();
            Player_Health player_Health = collision.GetComponent<Player_Health>();

            if (pMove.state == Player_Movement.StateOC.Dead)
            {
                return;
            }
            if (player_Health.isSliding)
            {
                if (All_Sounder.Instance != null && clipsForDodgedPlayer != null && clipsForDodgedPlayer.Length != 0)
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForDodgedPlayer);
                return;
            }


            if (player_Health.undamageableDelay < Time.timeSinceLevelLoad)
            {
                if (All_Sounder.Instance != null && clipsForDamagingPlayer != null && clipsForDamagingPlayer.Length != 0 && !player_Health.isSliding) 
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForDamagingPlayer);

                Animater();
                player_Health.GiveDamage();

                if (destroyOnCollision)
                {
                    DOTween.Kill(transform);
                    Destroy(gameObject);
                }
            }
        }
    }
    private void Animater()
    {
        Debug.Log("Animationer çalýþtý");
        if (animationer == null) return;

        Debug.Log("Animationer null'dan geçti");
        if (animater == animate.zombi)
        {
            Debug.Log("Animationer play attack dedi");
            animationer.Play("Attack2", 0, 0f);
        }
    }
}
