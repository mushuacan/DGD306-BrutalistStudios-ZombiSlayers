using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpit : MonoBehaviour
{
    private ZombiCharacter zombiChar;
    private bool canSpit;
    [SerializeField] private float rightCameraEdge;

    void Start()
    {
        canSpit = false;
        if (zombiChar.zombi.zombiAttidue == Scriptable_Zombies.zombiAttidues.Spitting) canSpit = true;
    }

    void Update()
    {
        if (transform.position.x < rightCameraEdge && canSpit) SpitAnimation();
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
        GameObject projectile = Instantiate(zombiChar.zombi.projectilePrefab);
        projectile.transform.position = transform.position;
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
