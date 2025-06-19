using DG.Tweening;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public float deathTimer = 3f;
    public ParticleSystem[] particles;
    public bool platformaYasla;
    private void OnEnable()
    {
        if (platformaYasla)
        {
            GameObject parentObj = GameObject.FindWithTag("Platform");
            if (parentObj != null)
            {
                transform.SetParent(parentObj.transform);
            }
        }

        foreach (var particle in particles)
        {
            if (particle != null)
            {
                if (!particle.gameObject.activeSelf)
                    particle.gameObject.SetActive(true);

                particle.Play();
            }
        }

        DOVirtual.DelayedCall(deathTimer, () =>
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }).SetUpdate(true);
    }
}
