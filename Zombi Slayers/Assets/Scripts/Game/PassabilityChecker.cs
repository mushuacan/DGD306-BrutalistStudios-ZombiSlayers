using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassabilityChecker : MonoBehaviour
{
    [SerializeField] private bool deleteIfNoFletcher;
    [SerializeField] private bool deleteIfNoWoods;
    [SerializeField] private bool deleteIfSinglePlayer;
    [SerializeField] private bool deleteIfNotSinglePlayer;

    // Start is called before the first frame update
    public void DestroyNoWoods()
    {
        if (deleteIfNoWoods) { Destroy(this.gameObject); }
    }
    public void DestroyNoFletcher()
    {
        if (deleteIfNoFletcher) { Destroy(this.gameObject); }
    }
    public void DestroyIfSingleplayer()
    {
        if (deleteIfSinglePlayer) { Destroy(this.gameObject); }
    }
    public void DestroyIfNotSingleplayer()
    {
        if (deleteIfNotSinglePlayer) { Destroy(this.gameObject); }
    }
}
