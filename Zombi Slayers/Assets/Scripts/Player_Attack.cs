using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private float delay;
    private bool canAttack;

    private void Start()
    {
        canAttack = true;
    }

    public void StartAttack(Scriptable_Weapons weapon)
    {
        if (canAttack)
        {
            canAttack = false;
            this.gameObject.GetComponent<Player_Movement>().state = Player_Movement.StateOC.Attacking;
            DOVirtual.DelayedCall(weapon.attackAnimationDuration, () => Attack(weapon));
        }
        if (delay <= Time.timeSinceLevelLoad)
        {
            canAttack = true;
        }
    }

    public void Attack(Scriptable_Weapons weapon)
    {
        if (weapon.weaponName == "Sledgehammer")
        {
            Instantiate(weapon.bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            delay = weapon.attackDelay + Time.timeSinceLevelLoad;
            this.gameObject.GetComponent<Player_Movement>().state = Player_Movement.StateOC.Running;
        }
    }
}
