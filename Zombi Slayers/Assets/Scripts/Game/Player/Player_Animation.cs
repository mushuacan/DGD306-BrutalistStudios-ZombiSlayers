using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player_Character player;

    public void StarterPack()
    {
        _spriteRenderer.sprite = player.character.spriter;
    }
}
