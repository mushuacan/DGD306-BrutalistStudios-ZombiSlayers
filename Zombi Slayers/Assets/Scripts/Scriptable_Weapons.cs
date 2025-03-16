using System.Collections;
using System.Collections.Generic;
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

    [Header("If it has bullets")]
    public bool haveBullets;
    [Tooltip("Silah�n �arj�r� bitince ka� saniyede �arj�r yenileyebilecek?")]
    public float reloadTime;
    [Tooltip("Bir �arj�rde ka� mermi olacak?")]
    public float bulletCount;


    [Header("Referances")]
    public GameObject bullet;

}