using DG.Tweening;
using UnityEngine;

public class Zombi_BOSS_JumpArea : MonoBehaviour
{
    public int bosslane;
    public Collider2D collider2d;

    public void ArrangeBossLane(int bossLane)
    {
        bosslane = bossLane;
    }
    public void OpenTrigger()
    {
        collider2d.enabled = true; 
        DOVirtual.DelayedCall(0.2f, () => Destroy(this.gameObject));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Zombi_BOSS_Jumped jumper = collision.GetComponent<Zombi_BOSS_Jumped>();
        if (jumper != null)
        {
            jumper.Savrul(bosslane);
        }
    }
}
