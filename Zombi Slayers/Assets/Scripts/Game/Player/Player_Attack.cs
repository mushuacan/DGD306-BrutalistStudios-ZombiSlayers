using UnityEngine;
using DG.Tweening;

public class Player_Attack : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Movement player_movement;
    private Scriptable_Weapons weapon;

    [Header("Gizli deðiþkenler")]
    [SerializeField] private float delay;
    [SerializeField] private bool canAttack;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int totalAmmo;


    private void Start()
    {
        canAttack = true;
        weapon = player.character.weapon;

        if (weapon.doesItReload)
        {
            currentAmmo = weapon.bulletCountinOneReload;
        }
        else
        {
            currentAmmo = weapon.bulletCountAtStart;
        }

        if (weapon.haveLimitedBullets)
        totalAmmo = weapon.bulletCountAtStart;
    }

    public void StartAttack()
    {
        CheckAttackStatus();

        if (canAttack)
        {
            canAttack = false;
            player_movement.action = Player_Movement.ActionOC.Attacking;
            player_UI.StartCastTimer(weapon.attackAnimationDuration);
            DOVirtual.DelayedCall(weapon.attackAnimationDuration, () => Attack());
            
        }
    }

    public void Attack()
    {
        GameObject bullet = Instantiate(weapon.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().Settings(weapon);
        ArrangeAttackCooldown();
        AmmoUI();
        player_movement.action = Player_Movement.ActionOC.Normal;
    }

    public void WoodsSecondAbility()
    {
        GameObject bullet = Instantiate(player.character.secondAbility.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        if (currentAmmo > 0)
        {
        }
    }
    public void FaoSecondAbility()
    {
        GameObject bullet = Instantiate(player.character.secondAbility.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.GetComponent<WindEffectController>().ArrangeReferance(player_movement);
    }
    private void CheckAttackStatus()
    {
        if (weapon.haveBullets)
        {
            if (currentAmmo > 0 && delay <= Time.timeSinceLevelLoad)
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
            currentAmmo--;
            if (currentAmmo <= 0)
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
        currentAmmo = weapon.bulletCountinOneReload;
        totalAmmo -= weapon.bulletCountinOneReload;
        AmmoUI();
    }
    private void AmmoUI()
    {
        player_UI.ArrangeAmmoCounter(currentAmmo, totalAmmo, weapon.haveBullets, weapon.haveLimitedBullets);
    }
}
