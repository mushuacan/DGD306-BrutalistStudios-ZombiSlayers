using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpitInAnimation : MonoBehaviour
{
    [SerializeField] private ZombiSpit spitter;

    public void CreateSpitBullet()
    {
        spitter.Spit();
    }

}
