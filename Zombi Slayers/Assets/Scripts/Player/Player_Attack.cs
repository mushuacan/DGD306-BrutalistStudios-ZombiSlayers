using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private Player_UI player_UI;

    private float delay;
    private bool canAttack;


    private void Start()
    {
        canAttack = true;
    }

    public void StartAttack(Scriptable_Weapons weapon)
    {
        if (delay <= Time.timeSinceLevelLoad)
        {
            canAttack = true;
        }
        if (canAttack)
        {
            canAttack = false;
            this.gameObject.GetComponent<Player_Movement>().action = Player_Movement.ActionOC.Attacking;
            player_UI.StartCastTimer(weapon.attackAnimationDuration);
            DOVirtual.DelayedCall(weapon.attackAnimationDuration, () => Attack(weapon));
        }
    }

    public void Attack(Scriptable_Weapons weapon)
    {
        if (weapon.weaponName == "Sledgehammer")
        {
            GameObject bullet = Instantiate(weapon.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().Settings(weapon);
            delay = weapon.attackDelay + Time.timeSinceLevelLoad;
            this.gameObject.GetComponent<Player_Movement>().action = Player_Movement.ActionOC.Normal;
        }
    }
}
