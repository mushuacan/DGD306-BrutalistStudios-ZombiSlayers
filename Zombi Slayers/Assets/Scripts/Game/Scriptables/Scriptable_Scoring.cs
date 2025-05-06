using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Scriptables/Score")]
public class Scriptable_Scoring : ScriptableObject
{
    public float scoreMultiplier;
    public int scoreEarnedPerSecond;
    public int scoreEarnedPerZombie;
}
