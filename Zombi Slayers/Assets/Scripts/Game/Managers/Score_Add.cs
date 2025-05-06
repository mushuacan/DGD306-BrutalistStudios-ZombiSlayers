using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Add : MonoBehaviour
{
    [SerializeField] private int scoreValue;
    void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
    }
}
