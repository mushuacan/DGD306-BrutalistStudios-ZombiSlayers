using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    public string weaponName;

    public BulletScript[] bulletsc;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject WoodsAttackExplosion;

    [SerializeField] private AudioClip[] clipsofHits;
    private Tween destroyTween;
    public void Settings(Scriptable_Weapons weapon)
    {
        damage = weapon.damage;
        weaponName = weapon.weaponName;

        //Fletcher's attack
        if (bulletsc != null)
        {
            BulletMaker(weapon);
        }

        //Woods' attack
        if (weapon.hitboxSize != new Vector2(0, 0))
        {
            boxCollider.size = weapon.hitboxSize;
            boxCollider.offset = weapon.hitboxOffset;
        }

        //Woods attack
        if (weaponName == "Sledgehammer")
        {
            Instantiate(WoodsAttackExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }

        destroyTween = DOVirtual.DelayedCall(weapon.BulletDestroyTimer, () =>
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }).SetUpdate(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombi"))
        {
            if (All_Sounder.Instance != null && clipsofHits.Length != 0)
            {
                All_Sounder.Instance.ChooseAndPlaySoundOf(clipsofHits, "Player Bullet Hit" + weaponName, true);
            }
        }
    }

    private void OnDestroy()
    {
        destroyTween.Kill();
    }
}
