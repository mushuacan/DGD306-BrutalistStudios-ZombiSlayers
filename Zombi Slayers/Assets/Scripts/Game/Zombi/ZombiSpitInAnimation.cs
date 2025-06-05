using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpitInAnimation : MonoBehaviour
{
    [SerializeField] private ZombiSpit spitter;
    [SerializeField] private CreateExplosionWhileDying exploMaker;

    public void CreateSpitBullet()
    {
        spitter.Spit();
    }

    public void CreateExplosion()
    {
        exploMaker.Explode();
    }

}
