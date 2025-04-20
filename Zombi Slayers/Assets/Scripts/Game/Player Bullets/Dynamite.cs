using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string[] tagsToDestroy;
    [SerializeField] private GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime ,transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTagAllowed(collision.tag))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            DOVirtual.DelayedCall(0.1f, () =>
            {
                Destroy(this.gameObject);
            });
        }
    }
    private bool IsTagAllowed(string objTag)
    {
        foreach (string tag in tagsToDestroy)
        {
            if (objTag == tag)
                return true;
        }
        return false;
    }
}
