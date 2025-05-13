using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassabilityChecker : MonoBehaviour
{
    [SerializeField] private bool deleteIfNoFletcher;
    [SerializeField] private bool deleteIfNoWoods;

    // Start is called before the first frame update
    public void DestroyNoWoods()
    {
        if (deleteIfNoWoods) { Destroy(this.gameObject); }
    }
    public void DestroyNoFletcher()
    {
        if (deleteIfNoFletcher) { Destroy(this.gameObject); }
    }
}
