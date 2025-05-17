using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosionWhileDying : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private bool _willExplode;
    [SerializeField] private float _atXPosition;

    private void Update()
    {
        if (_willExplode)
        {
            if (transform.position.x < _atXPosition)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}
