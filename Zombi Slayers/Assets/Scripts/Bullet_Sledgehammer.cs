using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Sledgehammer : MonoBehaviour
{
    private void OnEnable()
    {
        DOVirtual.DelayedCall( 1f, () => Destroy(this.gameObject));
    }
}
