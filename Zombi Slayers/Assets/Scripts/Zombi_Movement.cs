using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Movement : MonoBehaviour
{

    public float ZombiSpeed;
    public float WorldSpeed;
    public float xLeftEdge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - WorldSpeed * Time.deltaTime, transform.position.y);
        if (transform.position.x < xLeftEdge)
        {
            Destroy(gameObject);
        }
    }
}
