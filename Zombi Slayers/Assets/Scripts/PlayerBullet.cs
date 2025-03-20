using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    public string weaponName;

    public BulletScript[] bulletsc;

    public void Settings(Scriptable_Weapons weapon)
    {
        damage = weapon.damage;
        weaponName = weapon.weaponName;

        if (bulletsc != null )
        {
            BulletMaker(weapon);
        }

        DOVirtual.DelayedCall(weapon.BulletDestroyTimer, () => Destroy(this.gameObject));
    }

    public void BulletMaker(Scriptable_Weapons weapon)
    {
        foreach (BulletScript bullet in bulletsc)
        {
            if (bullet != null) // Null kontrolü yapmak önemli
            {
                bullet.isBulletMove = true;
                bullet.bulletSpeed = weapon.bulletSpeed;
                bullet.GetComponent<PlayerBullet>().damage = damage;
            }
        }
    }
}
