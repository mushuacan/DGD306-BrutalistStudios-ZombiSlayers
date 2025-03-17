using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage;

    public void Settings(Scriptable_Weapons weapon)
    {
        damage = weapon.damage;

        DOVirtual.DelayedCall(weapon.BulletDestroyTimer, () => Destroy(this.gameObject));
    }
}
