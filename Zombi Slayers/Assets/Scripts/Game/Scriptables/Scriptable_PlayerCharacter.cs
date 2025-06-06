using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "Scriptables/Player")]
public class Scriptable_PlayerCharacter : ScriptableObject
{
    public string characterName;
    public int health;
    public float secondAbilityTimer;
    public float secondAbilitySpeed;
    public float secondAbilityCooldown;
    public Scriptable_Weapons weapon;
    public Scriptable_Weapons secondAbility;
    public float animationSpeed = 1f;
    public float movementSpeed = 4f;

    public Texture imaj;
    public Sprite spriter;
    public Sprite secondAbilityIcon;
}
