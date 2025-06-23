using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiTheRunner : MonoBehaviour
{
    [SerializeField] private float _atXPosition;
    [SerializeField] private bool _running;
    [SerializeField] private float speed;
    [SerializeField] private Animator animationer;
    [SerializeField] private SpriteRenderer spriter;

    private void Start()
    {
        _running = false;
    }

    private void Update()
    {
        if (transform.position.x < _atXPosition)
        {
            _running = true;
            animationer.SetTrigger("Run");
            spriter.flipX = true;
        }
        if (_running)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
