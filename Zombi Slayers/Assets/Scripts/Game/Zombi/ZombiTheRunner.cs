using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiTheRunner : MonoBehaviour
{
    [SerializeField] private float _atXPosition;
    [SerializeField] private bool _running;
    [SerializeField] private float speed;

    private void Start()
    {
        _running = false;
    }

    private void Update()
    {
        if (transform.position.x < _atXPosition)
        {
            _running = true;
        }
        if (_running)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
