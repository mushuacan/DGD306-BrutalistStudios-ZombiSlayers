using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LaneMaker : MonoBehaviour
{
    [SerializeField] private bool snap;
    public int laneIndex;

    private void OnValidate()
    {
        SnapToNearestLaneX();
        snap = false;
        if (snap) { /*_*/ }
    }

    void SnapToNearestLaneX()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x / 19.2f) * 19.2f, 0, 0);
    }
}
