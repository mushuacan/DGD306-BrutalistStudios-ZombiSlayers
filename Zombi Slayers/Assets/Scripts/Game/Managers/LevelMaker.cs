using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public int level;
    public bool ArrangePassability;
    private bool isFletcherIn;
    private bool isWoodsIn;
    private bool isDerrickIn;
    [SerializeField] private ZombiAtTheBack_Manager zatbManager;

    // Chat GPT abim sað olsun (bir yazamadým aðzýný kýrdýðýmýnýný kodunu)
    private void Start()
    {
        isWoodsIn = false;
        isFletcherIn = false;
        isDerrickIn = false;

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
            if (player.character.characterName == "Derrick")
            {
                isDerrickIn = true;
            }
        }

        if (ArrangePassability)
        {        
            // Sahnedeki tüm PassabilityChecker bileþenlerini bul
            PassabilityChecker[] objects = FindObjectsOfType<PassabilityChecker>();

            // Her birine Arrangements fonksiyonunu uygula
            foreach (PassabilityChecker obj in objects)
            {
                if (!isWoodsIn) obj.DestroyNoWoods();
                if (!isFletcherIn) obj.DestroyNoFletcher();
                if (!isDerrickIn) obj.DestroyNoDerrick();
                if (players.Length == 1 && isDerrickIn) obj.DestroyIfOnlyDerrick();
                if (players.Length == 1) obj.DestroyIfSingleplayer();
                if (players.Length != 1) obj.DestroyIfNotSingleplayer();
            }
            if (zatbManager != null)
            {
                zatbManager.StopIfOnlyDerrick();
            }
        }
    }
}
