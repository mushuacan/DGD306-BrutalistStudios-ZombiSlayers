using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Scriptables/Zombie")]
public class Scriptable_Zombies : ScriptableObject
{
    public string zombieName;
    public int health;
    public bool canBeZombiATB;
    public zombiAttidues zombiAttidue;
    public float attackDuration;
    public float attackCooldown;
    public GameObject projectilePrefab;

    public enum zombiAttidues
    {
        None,
        Spitting,
        Attacking,
        kuduring
    }
}
