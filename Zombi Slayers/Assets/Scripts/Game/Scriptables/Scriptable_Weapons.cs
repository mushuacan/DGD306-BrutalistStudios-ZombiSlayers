using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptables/Weapon")]
public class Scriptable_Weapons : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Silah�n ismi")]
    public string weaponName;
    [Tooltip("Silah�n hasar�")]
    public int damage;
    [Tooltip("Oyuncu attack butonuna bast�ktan ka� saniye sonra tekrar basarsa tekrar attack yapabilir? (Attack Delay; Attack Animation bittikten sonra hesaplan�r)")]
    public float attackDelay;
    [Tooltip("Oyuncu attack butonuna bast�ktan ka� saniye sonra attack yapacak?")]
    public float attackAnimationDuration;
    [Tooltip("Mermi ka� saniye sonra yok olacak?")]
    public float BulletDestroyTimer;

    [Header("If it has bullets")]
    public bool haveBullets;
    [Tooltip("Yerden mermi toplamas� gerekiyor mu?")]
    public bool haveLimitedBullets;
    [Tooltip("Silah ile reload atmak gerekiyor mu?")]
    public bool doesItReload;
    [Tooltip("Oyun ba��nda ka� mermi ile ba�layacak?")]
    public int bulletCountAtStart;
    [Tooltip("Silah�n �arj�r� bitince ka� saniyede �arj�r yenileyebilecek?")]
    public float reloadTime;
    [Tooltip("Bir �arj�rde ka� mermi olacak?")]
    public int bulletCountinOneReload;
    [Tooltip("Ate�lenen merminin haritadaki h�z�?")]
    public float bulletSpeed;
    [Tooltip("Mermi hareketi nas�l olmal�?")]
    public bulletBehaviorTypes bulletBehavior;

    [Header("Hitbox Arrangements")]
    public Vector2 hitboxOffset;
    public Vector2 hitboxSize;

    [Header("Referances")]
    public GameObject bullet;
    public Sprite icon;

    public enum bulletBehaviorTypes
    {
        Nothing,
        Moving
    }
}