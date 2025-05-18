using UnityEngine;
using DG.Tweening;
using System;

public class Zombi_BOSS : MonoBehaviour
{
    private int currentActionIndex = 0;
    public int lane;
    public static event Action<int> JumpedEvent;
    [SerializeField] private Animator animator;
    [SerializeField] private ZombiAtTheBack_Manager zatbmanager;

    private void Awake()
    {
        if (zatbmanager != null)
        {
            zatbmanager.stopZombying = true;
        }
    }
    private void Start()
    {
        lane = 1;
        ExecuteNextAction();
    }

    private void ExecuteNextAction()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(UpdateType.Normal); // Time.timeScale'e baðlý olur

        sequence.AppendCallback(() => PerformAction(currentActionIndex));
        sequence.AppendInterval(2f); // Her hamle arasýnda 2 saniye bekle
        sequence.AppendCallback(() => {
            currentActionIndex = (currentActionIndex + 1) % 4;
            ExecuteNextAction();
        });
    }

    private void PerformAction(int index)
    {
        switch (index)
        {
            case 0:
                ChangeFloor();
                break;
            case 1:
                Spit();
                break;
            case 2:
                ZombieBall();
                break;
            case 3:
                RaiseSpikes();
                break;
        }
    }

    private void Spit()
    {
        animator.SetTrigger("Spit");
        Debug.Log("Boss spits!");
    }


    private void ZombieBall()
    {
        animator.SetTrigger("Ball");
        Debug.Log("Boss throws a zombie ball!");
    }

    private void ChangeFloor()
    {
        if (lane == 1) lane = lane + 1;
        else lane = lane - 1;
        animator.SetTrigger("Jump");
        transform.DOMoveY(LaneFinder.laneYPositions[lane], 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            Jumped();
            Debug.Log("Boss smashes and changes the floor!");
        });
    }

    private void RaiseSpikes()
    {
        Debug.Log("Boss raises spikes from the ground!");
    }

    public void Jumped()
    {
        JumpedEvent?.Invoke(lane);
        CameraShaker cameraShaker = Camera.main.GetComponent<CameraShaker>();
        if (cameraShaker != null)
        {
            cameraShaker.Shake();
        }
        else
        {
            Debug.LogWarning("Zombi Boss'un sarsýntýsý için gereken CameraShaker script'i halihazýrdaki sahnede bulunmamakta.");
        }
    }
}
