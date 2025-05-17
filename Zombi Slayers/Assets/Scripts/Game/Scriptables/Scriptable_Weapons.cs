using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptables/Weapon")]
public class Scriptable_Weapons : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Silahýn ismi")]
    public string weaponName;
    [Tooltip("Silahýn hasarý")]
    public int damage;
    [Tooltip("Oyuncu attack butonuna bastýktan kaç saniye sonra tekrar basarsa tekrar attack yapabilir? (Attack Delay; Attack Animation bittikten sonra hesaplanýr)")]
    public float attackDelay;
    [Tooltip("Oyuncu attack butonuna bastýktan kaç saniye sonra attack yapacak?")]
    public float attackAnimationDuration;
    [Tooltip("Mermi kaç saniye sonra yok olacak?")]
    public float BulletDestroyTimer;

    [Header("If it has bullets")]
    public bool haveBullets;
    [Tooltip("Yerden mermi toplamasý gerekiyor mu?")]
    public bool haveLimitedBullets;
    [Tooltip("Silah ile reload atmak gerekiyor mu?")]
    public bool doesItReload;
    [Tooltip("Oyun baþýnda kaç mermi ile baþlayacak?")]
    public int bulletCountAtStart;
    [Tooltip("Silahýn þarjörü bitince kaç saniyede þarjör yenileyebilecek?")]
    public float reloadTime;
    [Tooltip("Bir þarjörde kaç mermi olacak?")]
    public int bulletCountinOneReload;
    [Tooltip("Ateþlenen merminin haritadaki hýzý?")]
    public float bulletSpeed;
    [Tooltip("Mermi hareketi nasýl olmalý?")]
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