using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    public string weaponName;

    public BulletScript[] bulletsc;
    [SerializeField] private BoxCollider2D boxCollider;

    public void Settings(Scriptable_Weapons weapon)
    {
        damage = weapon.damage;
        weaponName = weapon.weaponName;

        if (bulletsc != null )
        {
            BulletMaker(weapon);
        }

        if (weapon.hitboxSize != new Vector2(0, 0))
        {
            boxCollider.size = weapon.hitboxSize;
            boxCollider.offset = weapon.hitboxOffset;
        }

        DOVirtual.DelayedCall(weapon.BulletDestroyTimer, () => Destroy(this.gameObject));
    }

    public void BulletMaker(Scriptable_Weapons weapon)
    {
        foreach (BulletScript bullet in bulletsc)
        {
            if (bullet != null) // Null kontrol� yapmak �nemli
            {
                bullet.isBulletMove = true;
                bullet.bulletSpeed = weapon.bulletSpeed;
                bullet.GetComponent<PlayerBullet>().damage = damage;
            }
        }
    }
}
