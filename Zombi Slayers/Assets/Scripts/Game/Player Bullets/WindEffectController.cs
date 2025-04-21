using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectController : MonoBehaviour
{
    [SerializeField] private Player_Movement pMovement;
    [SerializeField] private GameObject windPrefab;
    [SerializeField] private float lifetime;
    private float lifetimer;
    private void OnEnable()
    {
        lifetimer = Time.timeSinceLevelLoad + lifetime;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > lifetimer)
        {
            pMovement.ArrangeFaoWindStopBool(false);
            Collapse();
        }
        else if (Input.GetKeyDown(pMovement.moveDown_Button))
        {
            if (pMovement.lane == 1)
            {
                pMovement.ArrangeFaoWindStopBool(false);
                GameObject wind = CreateWind();
                wind.GetComponent<WindEffect>().ArrangeLane(3);
                Destroy(this.gameObject);
            }
            else
            {
                pMovement.ArrangeFaoWindStopBool(false);
                GameObject wind = CreateWind();
                wind.GetComponent<WindEffect>().ArrangeLane(pMovement.lane - 1);
                //if (!Input.GetKey(pMovement.second_Button))
                //    pMovement.JumpDown();
                pMovement.JumpDown();
                Destroy(this.gameObject);
            }
        }
        else if (Input.GetKeyDown(pMovement.moveUp_Button))
        {
            if (pMovement.lane == 3)
            {
                pMovement.ArrangeFaoWindStopBool(false);
                GameObject wind = CreateWind();
                wind.GetComponent<WindEffect>().ArrangeLane(1);
                Destroy(this.gameObject);
            }
            else
            {
                pMovement.ArrangeFaoWindStopBool(false);
                GameObject wind = CreateWind();
                wind.GetComponent<WindEffect>().ArrangeLane(pMovement.lane + 1);
                //if (!Input.GetKey(pMovement.second_Button))
                //    pMovement.JumpUp();
                pMovement.JumpUp();
                Destroy(this.gameObject);
            }
        }
    }
    private void Collapse()
    {
        Destroy(this.gameObject);
    }
    public void ArrangeReferance(Player_Movement player)
    {
        pMovement = player;
        pMovement.ArrangeFaoWindStopBool(true);
    }
    public GameObject CreateWind()
    {
        return Instantiate(windPrefab, transform.position, Quaternion.identity);
    }
}
