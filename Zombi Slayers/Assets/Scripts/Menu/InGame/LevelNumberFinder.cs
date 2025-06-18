using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumberFinder : MonoBehaviour
{
    public TextMeshProUGUI myText;

    void Start()
    {
        LevelMaker levelMaker = FindAnyObjectByType<LevelMaker>();
        int levelNumber = levelMaker.level;

        myText.text = "Level: " + levelNumber;
    }
}
