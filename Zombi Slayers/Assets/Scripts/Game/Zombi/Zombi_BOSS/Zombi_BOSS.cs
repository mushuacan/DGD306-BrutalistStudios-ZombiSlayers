using UnityEngine;
using DG.Tweening;

public class Zombi_BOSS : MonoBehaviour
{
    private int currentActionIndex = 0;
    public int lane;
    [SerializeField] private GameObject jumpArea;
    [SerializeField] private Animator animator;
    [SerializeField] private ZombiAtTheBack_Manager zatbmanager;
    [SerializeField] private Envoriment_Movement envoriment_Movement;
    [SerializeField] private AudioClip[] clipsForDamagingPlayer;
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
        float difficulty = (float)GameSettings.Instance.settings["difficulty"];
        float timer = 2 + (1 - difficulty) * 2;
        if (envoriment_Movement.sessionEnded == true)
        {
            animator.SetTrigger("Stand");
            return;
        }
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(UpdateType.Normal);

        sequence.AppendCallback(() => PerformAction(currentActionIndex));
        sequence.AppendInterval(timer);
        sequence.AppendCallback(() => {
            currentActionIndex = (currentActionIndex + 1) % 3;
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
        GameObject jumper = Instantiate(jumpArea);
        Zombi_BOSS_JumpArea jumperArea = jumper.GetComponent<Zombi_BOSS_JumpArea>();
        jumperArea.ArrangeBossLane(lane);
        jumperArea.OpenTrigger();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Movement pMove = collision.GetComponent<Player_Movement>();
            Player_Health player_Health = collision.GetComponent<Player_Health>();

            if (pMove.state == Player_Movement.StateOC.Dead)
            {
                return;
            }

            if (player_Health.undamageableDelay < Time.timeSinceLevelLoad)
            {
                if (All_Sounder.Instance != null && clipsForDamagingPlayer != null && clipsForDamagingPlayer.Length != 0 && !player_Health.isSliding)
                    All_Sounder.Instance.ChooseAndPlaySoundOf(clipsForDamagingPlayer, "Player Took damage by Zombi", false);
                player_Health.GiveDamage();
            }
        }
    }
}
