using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletScript : MonoBehaviour
{
    public bool isBulletMove;
    public float bulletSpeed; 
    public Vector2 moveDirection = Vector2.right; // Hareket yönü (yukarý)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBulletMove)
        {
            Vector2 direction = moveDirection.normalized;

            transform.Translate(direction * bulletSpeed * Time.deltaTime);
        }
    }
}
