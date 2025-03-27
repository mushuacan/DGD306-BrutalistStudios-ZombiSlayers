using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpit : MonoBehaviour
{
    [SerializeField] private ZombiCharacter zombiChar;
    [SerializeField] private float rightCameraEdge;
    private bool canSpit;

    void Start()
    {
        canSpit = false;
        if (zombiChar.zombi.zombiAttidue == Scriptable_Zombies.zombiAttidues.Spitting) canSpit = true;
        WaitForSpit();
    }

    private void WaitForSpit()
    {
        if (!canSpit) return;

        if (transform.position.x < rightCameraEdge)
        {
            SpitAnimation();
        }
        else
        {
            DOVirtual.DelayedCall(0.3f, () => { WaitForSpit(); });
        }
    }

    private void SpitAnimation()
    {
        DOVirtual.DelayedCall(zombiChar.zombi.attackDuration, () =>
        {
            Spit();
        });
    }

    private void Spit()
    {
        if (this.gameObject == null) return;

        GameObject projectile = Instantiate(zombiChar.zombi.projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<ZombiBullet>().StartMoving();
        SpitCooldown();
    }

    private void SpitCooldown()
    {
        DOVirtual.DelayedCall(zombiChar.zombi.attackCooldown, () =>
        {
            SpitAnimation();
        });
    }
}
