using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class PlayerManager_Counter : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput player)
    {
        GameObject pManager = GameObject.FindGameObjectWithTag("PlayerManager");
        if (pManager != null)
            pManager.GetComponent<PlayerManager>().OnPlayerJoined(player);
    }
}
