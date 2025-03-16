using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "Scriptables/Player")]
public class Scriptable_PlayerCharacter : ScriptableObject
{
    public string characterName;
    public int health;
    public Scriptable_Weapons weapon;
}
