using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public int level;
    public bool ArrangePassability;
    private bool isFletcherIn;
    private bool isWoodsIn;

    // Chat GPT abim sa� olsun (bir yazamad�m a�z�n� k�rd���m�n�n� kodunu)
    private void Start()
    {
        isWoodsIn = false;
        isFletcherIn = false;

        Player_Character[] players = FindObjectsOfType<Player_Character>();
        foreach (Player_Character player in players)
        {
            if  (player.character.characterName == "Fletcher")
            {
                isFletcherIn = true;
            }
            if (player.character.characterName == "Woods")
            {
                isWoodsIn = true;
            }
        }

        if (ArrangePassability)
        {        
            // Sahnedeki t�m PassabilityChecker bile�enlerini bul
            PassabilityChecker[] objects = FindObjectsOfType<PassabilityChecker>();

            // Her birine Arrangements fonksiyonunu uygula
            foreach (PassabilityChecker obj in objects)
            {
                if (!isWoodsIn) obj.DestroyNoWoods();
                if (!isFletcherIn) obj.DestroyNoFletcher();
                if (players.Length == 1) obj.DestroyIfSingleplayer();
                if (players.Length != 1) obj.DestroyIfNotSingleplayer();
            }
        }
    }
}
