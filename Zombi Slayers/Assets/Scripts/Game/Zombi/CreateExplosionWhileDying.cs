using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosionWhileDying : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Animator animationer;
    [SerializeField] private bool _willExplode;
    [SerializeField] private float _atXPosition;
    [SerializeField] private bool exploded;
    [SerializeField] private AudioClip[] clips;

    private void Update()
    {
        if (_willExplode && !exploded)
        {
            if (transform.position.x < _atXPosition)
            {
                exploded = true;
                ExplodeAnimation();
            }
        }
    }
    public void ExplodeAnimation()
    {
        animationer.Play("Bomb", 0, 0f);
    }
    public void Explode()
    {
        if (All_Sounder.Instance != null && clips.Length != 0) All_Sounder.Instance.ChooseAndPlaySoundOf(clips, "Zombi Explosion", true);
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
