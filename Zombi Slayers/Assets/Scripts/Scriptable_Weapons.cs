using System.Collections;
using System.Collections.Generic;
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

    [Header("If it has bullets")]
    public bool haveBullets;
    [Tooltip("Silahýn þarjörü bitince kaç saniyede þarjör yenileyebilecek?")]
    public float reloadTime;
    [Tooltip("Bir þarjörde kaç mermi olacak?")]
    public float bulletCount;


    [Header("Referances")]
    public GameObject bullet;

}