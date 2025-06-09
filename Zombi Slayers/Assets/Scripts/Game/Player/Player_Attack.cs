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
            currentAmmo = weapon.bulletCountInGunAtStart;
            if (currentAmmo == 0)
            {
                canAttack = false;
            }
        }
        else
        {
            currentAmmo = weapon.bulletMagazineAtStart;
        }


        if (player.character.secondAbility != null)
        {
            if (player.character.secondAbility.haveLimitedBullets)
            {
                dynamiteAmmo = player.character.secondAbility.bulletMagazineAtStart;
                player_UI.ArrangeSecondAbilityCounter(dynamiteAmmo, true);
            }
        }

        if (weapon.haveLimitedBullets)
        {
            totalAmmo = weapon.bulletMagazineAtStart;

            if ((float)GameSettings.Instance.settings["difficulty"] == 0f)
            {
                if (player.character.characterName == "Fletcher")
                {
                    totalAmmo += 4;
                }
                totalAmmo += 4;
            }

            if ((float)GameSettings.Instance.settings["difficulty"] == 0.5f)
            {
                if (player.character.characterName == "Fletcher")
                {
                    totalAmmo += 2;
                }
                totalAmmo += 2;
            }

        }
        AmmoUI();
    }

    public void StartAttack()
    {
        CheckAttackStatus();


        if (canAttack)
        {
            if (player_movement.animations) player_movement.player_animation.Attack();
            canAttack = false;
            player_movement.action = Player_Movement.ActionOC.Attacking;
            if(weapon.weaponName != "Sniper") player_UI.StartCastTimer(weapon.attackAnimationDuration);
            player_UI.WeaponUsing();

            // Bu denklem için uðraþtým biraz ancak istediðim þeyi yapamadým. Sonra da bu denklemi yapay zekadan istedim ve þak diye verdi.
            // Bence artýk matematik bilmenin ya da bilmemenin bir önemi kalmadý. Yapay zekadan neyi nasýl isteyeceðini bilmek gerek sadece, tabi bu bencesi yani.
            // Burdaki kodun iþlevi de bu arada eðer oyun modunun zorluðu kolay (0) ise vuruþ süresi 0.75x oluyor, ha yok zor (1) ise 1x 'te (normal süresi kadar) sürüyor.
            float difficulty = (float)GameSettings.Instance.settings["difficulty"];
            float animationDuration = 0.25f * difficulty + 0.75f;

            DOVirtual.DelayedCall(weapon.attackAnimationDuration * animationDuration, () => Attack());
            
        }
    }

    public void Attack()
    {
        if (player_movement.animations) player_movement.player_animation.Attacked();
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
    public void DerrickSecondAbility()
    {
        if (player.character.secondAbility == null) return;
        player_UI.StartCastTimer(player.character.secondAbility.attackAnimationDuration);
        DOVirtual.DelayedCall(player.character.secondAbility.attackAnimationDuration, () =>
        {
            if (player_movement.state == Player_Movement.StateOC.Dead) return;
            if (player_movement.animations) player_movement.player_animation.SecondAbility();
            GameObject bullet = Instantiate(player.character.secondAbility.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().Settings(player.character.secondAbility);
        });
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
        float difficulty = (float)GameSettings.Instance.settings["difficulty"];
        float animationDuration = 0.25f * difficulty + 0.75f;
        float calculatedAttackDelay = player.character.weapon.attackDelay * animationDuration;
        Debug.Log("Vuruþ süresi: " + calculatedAttackDelay);

        player_UI.StartWeaponCooldown(calculatedAttackDelay);
        delay = calculatedAttackDelay + Time.timeSinceLevelLoad;
    }

    private void Reload()
    {
        float difficulty = (float)GameSettings.Instance.settings["difficulty"];
        float animationDuration = 0.25f * difficulty + 0.75f;
        float calculatedAttackDelay = player.character.weapon.reloadTime * animationDuration;

        player_UI.WeaponUsing();
        player_UI.StartWeaponCooldown(calculatedAttackDelay);
        DOVirtual.DelayedCall(calculatedAttackDelay, () => Reloaded());
        
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
        if ( currentAmmo == 0)
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
