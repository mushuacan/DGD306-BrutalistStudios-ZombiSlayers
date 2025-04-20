using UnityEngine;
using DG.Tweening;

public class ExplosionZone : MonoBehaviour
{
    public BoxCollider2D colliderComp;
    public float delayBeforeExplode = 0.05f;
    public float lifetime = 0.5f;
    public LayerMask targetLayer;
    public string[] tagsToDestroy;

    private void Start()
    {
        // Belirli süre sonra patlat
        DOVirtual.DelayedCall(delayBeforeExplode, () =>
        {
            Destroy(gameObject);
        });

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTagAllowed(collision.tag))
        {
            Destroy(collision.gameObject);
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
