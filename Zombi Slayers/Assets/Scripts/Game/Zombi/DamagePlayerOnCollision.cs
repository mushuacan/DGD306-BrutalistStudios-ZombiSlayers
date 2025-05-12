using UnityEngine;
using DG.Tweening;

public class DamagePlayerOnCollision : MonoBehaviour
{
    [SerializeField] private bool destroyOnCollision;
    [Tooltip("For only bullets")]
    [SerializeField] private bool canSlideable;
    [SerializeField] private AudioClip[] clipsForDamagingPlayer;
    [SerializeField] private bool isThisZombiBullet = false;
    [SerializeField] private AudioClip[] clipsForDodgedPlayer;
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
                player_Health.GiveDamage();

                if (destroyOnCollision)
                {
                    DOTween.Kill(transform);
                    Destroy(gameObject);
                }
            }
        }
    }
}
