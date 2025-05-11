using UnityEngine;
using DG.Tweening;

public class ExplosionZone : MonoBehaviour
{
    public GameObject ExplosionAnimationPrefab;
    public BoxCollider2D colliderComp;
    public float delayBeforeExplode = 0.05f;
    public float lifetime = 0.5f;
    public LayerMask targetLayer;
    public string[] tagsToDestroy;

    private void Start()
    {
        CameraShaker cameraShaker = Camera.main.GetComponent<CameraShaker>();
        if (cameraShaker != null )
        {
            cameraShaker.Shake();
        }
        else
        {
            Debug.LogWarning("Dinamit Patlamasý için gereken CameraShaker script'i halihazýrdaki sahnede bulunmamakta.");
        }
        Instantiate(ExplosionAnimationPrefab, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z ), Quaternion.identity);
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
