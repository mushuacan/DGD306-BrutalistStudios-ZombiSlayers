using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envoriment_Movement_Endless : MonoBehaviour
{
    [SerializeField] private float envorimentMovementSpeed;
    [SerializeField] private Transform prefabric;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - (envorimentMovementSpeed * Time.deltaTime), transform.position.y, 5);
        if (transform.position.x < -38)
        {
            DestroyAllChildren();
            transform.position = new Vector3(transform.position.x + (19.19f * 4), transform.position.y, transform.position.z);
        }
    }
    public void IncreaseMovementSpeed(float Increasement)
    {
        envorimentMovementSpeed += Increasement;
    }
    public float GetEnvorimentSpeed()
    {
        return envorimentMovementSpeed;
    }
    void DestroyAllChildren()
    {
        foreach (Transform child in prefabric)
        {
            Destroy(child.gameObject);
        }
    }

}
