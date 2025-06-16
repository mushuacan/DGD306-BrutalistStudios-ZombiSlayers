using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    private PlayerManager playerManager;
    private Player_Inputs p1, p2;
    private bool pressedAtBeggining;
    private float waitStartTime;
    void Start()
    {
        pressedAtBeggining = true;
        Time.timeScale = 0.0f;
        playerManager = FindAnyObjectByType<PlayerManager>();
        if (playerManager != null )
        {
            GameSettings.Instance.settings["gizem"] = true;
            p1 = playerManager.players[0].GetComponent<Player_Inputs>();
            if (playerManager.playerCount == 2 )
                p2 = playerManager.players[1].GetComponent<Player_Inputs>();
        }
        else
        {
            p1 = FindAnyObjectByType<Player_Inputs>();
        }

        if ((int)GameSettings.Instance.settings["level"] == FindAnyObjectByType<LevelMaker>().level - 1)
        {
            if ((bool)GameSettings.Instance.settings["isThisLevelNew"])
            {
                waitStartTime = Time.realtimeSinceStartup + 2f;
                GameSettings.Instance.settings["isThisLevelNew"] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( pressedAtBeggining)
        {
            if (p2 == null)
            {
                if(p1 != null)
                {
                    if (!p1.button0pressed)
                    {
                        pressedAtBeggining = false;
                    }
                }
            }
            else
            {
                if (!p1.button0pressed && !p2.button0pressed)
                {
                    pressedAtBeggining = false;
                }
            }
        }
        else
        {
            if (waitStartTime < Time.realtimeSinceStartup)
            {
                if (p2 == null)
                {
                    if (p1.button0pressed)
                    {
                        Time.timeScale = 1.0f;
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (p1.button0pressed || p2.button0pressed)
                    {
                        Time.timeScale = 1.0f;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
