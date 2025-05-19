// Yapay zekaya baþtan yazdýrýldý.

using UnityEngine;
using System.Collections;

public class ZombiSpit : MonoBehaviour
{
    [SerializeField] private ZombiCharacter zombiChar;
    [SerializeField] private Transform platform;
    [SerializeField] private float rightCameraEdge;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private Collider2D collider2d;

    private bool canSpit;
    private Coroutine spitRoutine;

    void Start()
    {
        canSpit = zombiChar.zombi.zombiAttidue == Scriptable_Zombies.zombiAttidues.Spitting;

        if (canSpit)
        {
            spitRoutine = StartCoroutine(SpitLogic());
        }
    }

    private IEnumerator SpitLogic()
    {
        // Ekrana girene kadar bekle
        yield return new WaitUntil(() => transform.position.x < rightCameraEdge);

        // Sürekli saldýrý döngüsü
        while (true)
        {
            yield return new WaitForSeconds(zombiChar.zombi.attackDuration);

            if (collider2d.enabled)
            {
                All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
                Spit();
            }

            yield return new WaitForSeconds(zombiChar.zombi.attackCooldown);
        }
    }

    private void Spit()
    {
        GameObject projectile = Instantiate(zombiChar.zombi.projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<ZombiBullet>().StartMoving();
        projectile.transform.SetParent(platform);
    }

    private void OnDestroy()
    {
        if (spitRoutine != null)
        {
            StopCoroutine(spitRoutine);
        }
    }
}
