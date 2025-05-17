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
    [SerializeField] private int dynamiteAmmo;


    public void StarterPack()
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


        if (player.character.secondAbility != null)
        {
            if (player.character.secondAbility.haveLimitedBullets)
            {
                dynamiteAmmo = player.character.secondAbility.bulletCountAtStart;
                player_UI.ArrangeSecondAbilityCounter(dynamiteAmmo, true);
            }
        }

        if (weapon.haveLimitedBullets)
            totalAmmo = weapon.bulletCountAtStart;
    }

    public void StartAttack()
    {
        CheckAttackStatus();


        if (canAttack)
        {
            if (player_movement.animations) player_movement.player_animation.Attack();
            canAttack = false;
            player_movement.action = Player_Movement.ActionOC.Attacking;
            player_UI.StartCastTimer(weapon.attackAnimationDuration);
            DOVirtual.DelayedCall(weapon.attackAnimationDuration, () => Attack());
            
        }
    }

    public void Attack()
    {
        player_movement.player_sounder.PlayAttackSound();
        GameObject bullet = Instantiate(weapon.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().Settings(weapon);
        ArrangeAttackCooldown();
        AmmoUI();
        player_movement.action = Player_Movement.ActionOC.Normal;
    }

    public void WoodsSecondAbility()
    {
        if (dynamiteAmmo > 0)
        {
            if (player_movement.animations) player_movement.player_animation.SecondAbility();
            dynamiteAmmo--;
            player_UI.ArrangeSecondAbilityCounter(dynamiteAmmo, true);
            player_UI.StartCastTimer(player.character.secondAbility.attackAnimationDuration);
            DOVirtual.DelayedCall(player.character.secondAbility.attackAnimationDuration, () =>
            Instantiate(player.character.secondAbility.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity));
        }
    }
    public void FaoSecondAbility()
    {
        GameObject bullet = Instantiate(player.character.secondAbility.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.GetComponent<WindEffectController>().ArrangeReferance(player_movement);
    }
    private void CheckAttackStatus()
    {
        if (weapon == null)
        {
            weapon = player.character.weapon;
        }

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
                Reload();
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
        player_UI.StartWeaponCooldown(player.character.weapon.attackDelay);
        delay = player.character.weapon.attackDelay + Time.timeSinceLevelLoad;
    }

    private void Reload()
    {
        player_UI.StartWeaponCooldown(player.character.weapon.reloadTime);
        DOVirtual.DelayedCall(player.character.weapon.reloadTime, () => Reloaded());
        
    }
    private void Reloaded()
    {
        int reloader = weapon.bulletCountinOneReload;
        int currentAmmoAtFirst = currentAmmo;
        if (totalAmmo > reloader)
        {
            player_movement.player_sounder.PlayFletchersReload();
            currentAmmo = reloader;
            totalAmmo -= reloader;
        }
        else if (totalAmmo > 0)
        {
            player_movement.player_sounder.PlayFletchersReload();
            currentAmmo = totalAmmo;
            totalAmmo = 0;
        }
        if (currentAmmoAtFirst > 0)
        {
            totalAmmo += currentAmmoAtFirst;
        }
        AmmoUI();
    }

    public void TakeMagazine(int magazines)
    {
        totalAmmo += magazines;
        if ( totalAmmo == magazines)
        {
            Reload();
        }
        AmmoUI();
    }
    public void TakeDynamite()
    {
        dynamiteAmmo++;
        player_UI.ArrangeSecondAbilityCounter(dynamiteAmmo, true);
    }

    private void AmmoUI()
    {
        player_UI.ArrangeAmmoCounter(currentAmmo, totalAmmo, weapon.haveBullets, weapon.haveLimitedBullets);
    }
}
