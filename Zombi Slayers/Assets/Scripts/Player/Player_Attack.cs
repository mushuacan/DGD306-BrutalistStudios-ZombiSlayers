using UnityEngine;
using DG.Tweening;

public class Player_Attack : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Movement player_movement;

    [Header("Gizli deðiþkenler")]
    [SerializeField] private float delay;
    [SerializeField] private bool canAttack;
    [SerializeField] private int bulletCount;


    private void Start()
    {
        canAttack = true;
        bulletCount = player.character.weapon.bulletCount;
    }

    public void StartAttack()
    {
        CheckAttackStatu();

        if (canAttack)
        {
            canAttack = false;
            player_movement.action = Player_Movement.ActionOC.Attacking;
            player_UI.StartCastTimer(player.character.weapon.attackAnimationDuration);
            DOVirtual.DelayedCall(player.character.weapon.attackAnimationDuration, () => Attack());
            
        }
    }

    public void Attack()
    {
        GameObject bullet = Instantiate(player.character.weapon.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().Settings(player.character.weapon);
        ArrangeAttackCooldown();
        player_movement.action = Player_Movement.ActionOC.Normal;
    }

    private void CheckAttackStatu()
    {
        if (player.character.weapon.haveBullets)
        {
            if (bulletCount > 0 && delay <= Time.timeSinceLevelLoad)
            {
                canAttack = true;
            }
        }

        else
        {   
            if (delay <= Time.timeSinceLevelLoad)
            {
                canAttack = true;
            }
        }
    }

    private void ArrangeAttackCooldown()
    {
        if (player.character.weapon.haveBullets)
        {
            bulletCount--;
            if (bulletCount <= 0)
            {
                player_UI.StartCooldown(player.character.weapon.reloadTime);
                DOVirtual.DelayedCall(player.character.weapon.reloadTime, () => Reload());
            }
            else
            {
                SetDelay();
            }
        }
        else
        {
            SetDelay();
        }
    }

    private void SetDelay()
    {
        player_UI.StartCooldown(player.character.weapon.attackDelay);
        delay = player.character.weapon.attackDelay + Time.timeSinceLevelLoad;
    }

    private void Reload()
    {
        bulletCount = player.character.weapon.bulletCount;
    }
}
