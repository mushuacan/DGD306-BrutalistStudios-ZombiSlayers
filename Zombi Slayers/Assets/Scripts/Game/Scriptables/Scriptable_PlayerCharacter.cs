using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "Scriptables/Player")]
public class Scriptable_PlayerCharacter : ScriptableObject
{
    public string characterName;
    public int health;
    public float slideTimer;
    public float slideSpeed;
    public float slideCooldown;
    public Scriptable_Weapons weapon;
}
