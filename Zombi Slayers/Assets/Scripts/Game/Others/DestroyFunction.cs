using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFunction : MonoBehaviour
{
    public bool shouldItBeTimed;
    public float timer;
    public void DestroyThis() {  Destroy(gameObject); }
    public void DestroyWithParent() { Destroy(transform.parent.gameObject); }

    private void OnEnable()
    {
        if (shouldItBeTimed)
        {
            DOVirtual.DelayedCall(timer, () =>
            {
                Destroy(gameObject);
            })
            .SetUpdate(true);
        }
    }
}
