using UnityEngine;
using DG.Tweening;

public class ZombiSpit : MonoBehaviour
{
    [SerializeField] private ZombiCharacter zombiChar;
    [SerializeField] private Transform platform;
    [SerializeField] private float rightCameraEdge;
    [SerializeField] private AudioClip[] clips;
    private bool canSpit;
    private Tween spitTween;


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
            spitTween = DOVirtual.DelayedCall(0.3f, () => { WaitForSpit(); }).SetUpdate(UpdateType.Normal);
        }
    }

    private void SpitAnimation()
    {
        spitTween = DOVirtual.DelayedCall(zombiChar.zombi.attackDuration, () =>
        {
            All_Sounder.Instance.ChooseAndPlaySoundOf(clips);
            Spit();
        }).SetUpdate(UpdateType.Normal);
    }

    private void Spit()
    {
        GameObject projectile = Instantiate(zombiChar.zombi.projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<ZombiBullet>().StartMoving();
        projectile.transform.SetParent(platform);
        SpitCooldown();
    }

    private void SpitCooldown()
    {
        spitTween = DOVirtual.DelayedCall(zombiChar.zombi.attackCooldown, () =>
        {
            SpitAnimation();
        }).SetUpdate(UpdateType.Normal);
    }

    private void OnDestroy()
    {
        spitTween.Kill();
    }
}
