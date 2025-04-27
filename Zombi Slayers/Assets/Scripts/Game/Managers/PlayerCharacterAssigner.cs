using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterAssigner : MonoBehaviour
{
    public Scriptable_PlayerCharacter[] availableCharacters; // Inspector'dan atayacaðýz

    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        var playerCharacter = GetComponent<Player_Character>();

        if (playerInput != null && playerCharacter != null)
        {
            int playerIndex = playerInput.playerIndex;

            if (playerIndex >= 0 && playerIndex < availableCharacters.Length)
            {
                playerCharacter.character = availableCharacters[playerIndex];
                Debug.Log($"Player {playerIndex} assigned to {availableCharacters[playerIndex].name}");
            }
            else
            {
                Debug.LogWarning("Player index out of bounds for available characters list.");
            }
        }
        else
        {
            Debug.LogError("Missing components on player prefab!");
        }
    }
}
