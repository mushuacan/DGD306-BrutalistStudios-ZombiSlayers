using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFunction : MonoBehaviour
{
    public void DestroyThis() {  Destroy(gameObject); }
    public void DestroyWithParent() { Destroy(transform.parent.gameObject); }

}
