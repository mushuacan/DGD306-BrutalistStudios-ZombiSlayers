using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFinder : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject parentObj = GameObject.FindWithTag("Platform");
        if (parentObj != null)
        {
            transform.SetParent(parentObj.transform);
        }
    }
}
