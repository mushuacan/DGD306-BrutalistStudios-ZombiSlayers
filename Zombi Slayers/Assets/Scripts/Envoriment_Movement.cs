using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envoriment_Movement : MonoBehaviour
{
    public float envorimentMovementSpeed;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - (envorimentMovementSpeed * Time.deltaTime), transform.position.y, 5);
    }
}
