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

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }
    }
}
