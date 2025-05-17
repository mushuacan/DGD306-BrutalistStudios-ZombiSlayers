using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Player_Movement : MonoBehaviour
{
    #region Variables

    public bool carryToDDOL;

    [Header("BUTTONS")]
    public bool openButtons;
    [SerializeField] public KeyCode moveUp_Button;
    [SerializeField] public KeyCode moveDown_Button;
    [SerializeField] private KeyCode moveRight_Button;
    [SerializeField] private KeyCode moveLeft_Button;
    [SerializeField] private KeyCode attack_Button;
    [SerializeField] public KeyCode second_Button;

    [Header("Referances")]
    [SerializeField] public Player_Animation player_animation;
    [SerializeField] public Player_Sounder player_sounder;
    [SerializeField] private Player_Inputs inputs;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Attack player_attack;
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Health player_health;
    [SerializeField] private Scriptable_PlayerCharacter fixedPlayerCharacter;
    [SerializeField] private GameObject DeadLocation;
    private GameObject platform;

    [Header("Playground Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;

    [Header("Animation Settings")]
    [SerializeField] public bool animations;
    [SerializeField] private float animationSpeed;
    [SerializeField] private float jumpAnimationDuration;
    [SerializeField] private float jumpAnimationUpDistance;

    [Header("Starting")]
    [SerializeField] [Range(-8, 5)] private int startPositionX;
    [SerializeField] [Range(1, 3)] public int lane;
    private float[] laneYPoz = { -1f, -3f, 0.25f, 3.5f };

    [Header("(private variables)")]
    [SerializeField] public StateOC state;
    [SerializeField] public ActionOC action;
    private float secondAbilityCooldown;
    [SerializeField] private bool FaoWind_StopJump;
    [SerializeField] private bool FaoWind_JumpedNew;
    [SerializeField] private bool FaoWind_JumpedUp;
    public bool isPlayingNow;
    private GameObject DeadLocationer;
    private bool exitedMenuNew;
    private float exitedTime;
    private Tween jumpTween;

    #endregion

    #region enums

    public enum StateOC // State of Character
    {
        Running,
        Jumping,
        Dead,
        EndGame
    }
    public enum ActionOC
    {
        Normal,
        Attacking,
        SecondAbility,
        cannot
    }

    #endregion

    #region Functions

    #region Start & Update

    private void Awake()
    {
        if (carryToDDOL) CarryPlayerToDDOL();
    }

    public void CarryPlayerToDDOL()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CheckIfGameRuns();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlayingNow)
            return;
        if (state == StateOC.Dead)
        {
            if (jumpTween != null && jumpTween.IsActive() && jumpTween.IsPlaying())
            {
                transform.position = new Vector3(DeadLocationer.transform.position.x, transform.position.y, transform.position.z);
                return;
            }
            if (DeadLocationer != null) { transform.position = DeadLocationer.transform.position; }
            return;
        }
        if ( Time.timeScale == 0)
        {
            exitedTime = Time.timeSinceLevelLoad + 1f;
            exitedMenuNew = true;
            return;
        }
        if (exitedMenuNew)
        {
            if (inputs.button0pressed)
            {
                return;
            }
            else if (exitedTime < Time.timeSinceLevelLoad)
            {
                exitedMenuNew = false;
            }
            else
            {
                exitedMenuNew = false; 
            }
        }

        CheckIfDead();
        ArrangeJumping();
        ArrangeMovement();
        CheckButtons();
        CheckESCMenu();
        CheckExit();
    }

    #endregion

    #region Movement Functions
    private void CheckESCMenu()
    {
        if (inputs.startPressed)
        {
            //Debug.Log("Started basýldý");
            FindAnyObjectByType<InGameMenuManager>().OpenESCMenu();
        }
    }
    private void CheckExit()
    {
        if (inputs.startPressed && inputs.sellectPressed)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
    private void CheckIfGameRuns()
    {
        ZombiAtTheBack_Manager zatbm = FindObjectOfType<ZombiAtTheBack_Manager>();
        if (zatbm != null)
        {
            isPlayingNow = true;
            StarterPack();
        }
        else
        {
            isPlayingNow = false;
        }
    }

    public void StarterPack()
    {
        state = StateOC.Running;
        action = ActionOC.Normal;

        laneYPoz = LaneFinder.laneYPositions;

        transform.position = new Vector2(startPositionX, laneYPoz[lane]);

        platform = GameObject.FindWithTag("Platform");

        if (player.character == null)
        {
            player.character = fixedPlayerCharacter;
        }

        animations = (bool)GameSettings.Instance.settings["animations"];

        secondAbilityCooldown = 0f;
        FaoWind_StopJump = false;

        player_health.StarterPack();
        player_attack.StarterPack();
        player_UI.StarterPack();
        player_animation.StarterPack();
    }

    private void ArrangeJumping()
    {
        if (FaoWind_StopJump)
        {
            return;
        }
        if (FaoWind_JumpedNew)
        {
            if (FaoWind_JumpedUp && Input.GetKey(moveUp_Button) == false)
                FaoWind_JumpedNew = false;
            if (!FaoWind_JumpedUp && Input.GetKey(moveDown_Button) == false)
                FaoWind_JumpedNew = false;
            return;
        }
        if (((Input.GetKey(moveUp_Button) && openButtons) || inputs.MovementValues.y > 0.5) && state == StateOC.Running)
        {
            if (lane == 3)
                return;
            else
            {
                JumpBetweenLanes("Up");
                if (animations) player_animation.Jump();
            }
        }
        if (((Input.GetKey(moveDown_Button) && openButtons) || inputs.MovementValues.y < -0.5) && state == StateOC.Running)
        {
            if (lane == 1)
                return;
            else
            {
                JumpBetweenLanes("Down");
                if (animations) player_animation.Jump();
            }
        }
    }

    private void ArrangeMovement()
    {
        if (action == ActionOC.SecondAbility && player.character.characterName == "Fletcher")
        {
            if (transform.position.x < rightBoundary)
                transform.position = new Vector2(transform.position.x + player.character.secondAbilitySpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            if (((Input.GetKey(moveRight_Button) && openButtons) || inputs.MovementValues.x > 0.5) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x < rightBoundary)
                {
                    transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
                    if (animations) player_animation.Run();
                }
            }

            else if (((Input.GetKey(moveLeft_Button) && openButtons) || inputs.MovementValues.x < -0.5) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x > leftBoundary)
                {
                    transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
                    if (animations) player_animation.Walk();
                }
            }
            else
            {
                if (animations) player_animation.Run();
            }
        }
    }
    private void CheckButtons()
    {
        if (((Input.GetKeyDown(attack_Button) && openButtons) || inputs.button0pressed ) && action == ActionOC.Normal)
        {
            Attack();
        }
        if (((Input.GetKeyDown(second_Button) && openButtons) || inputs.button1pressed ) && action == ActionOC.Normal && state != StateOC.Jumping && secondAbilityCooldown < Time.timeSinceLevelLoad)
        {
            action = ActionOC.SecondAbility;

            SecondAbility();

            if (player.character.secondAbility == null)
            {
                DOVirtual.DelayedCall(player.character.secondAbilityTimer, () =>
                {
                    action = ActionOC.Normal;
                    secondAbilityCooldown = player.character.secondAbilityCooldown + Time.timeSinceLevelLoad;
                    player_UI.StartSecondAbilityCooldown(player.character.secondAbilityCooldown);
                });
            }
            else
            {
                DOVirtual.DelayedCall(player.character.secondAbility.attackAnimationDuration, () =>
                {
                    action = ActionOC.Normal;
                    secondAbilityCooldown = player.character.secondAbilityCooldown + Time.timeSinceLevelLoad;
                    player_UI.StartSecondAbilityCooldown(player.character.secondAbilityCooldown);
                });
            }
        }
    }

    public void JumpBetweenLanes(string Vertical)
    {
        if (Vertical == "Down")
        {
            JumpDown();
        }
        if (Vertical == "Up")
        {
            JumpUp();
        }
    }
    public void JumpDown()
    {
        state = StateOC.Jumping;
        lane = lane - 1;
        player_sounder.PlayJumpSound();
        jumpTween = transform.DOMoveY(jumpAnimationUpDistance + transform.position.y, (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            jumpTween = transform.DOMoveY(laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                player_sounder.PlayLandSound();
                if (state == StateOC.EndGame) return;
                if (state == StateOC.Dead) return;
                state = StateOC.Running;
            });
        });
    }
    public void JumpUp()
    {
        state = StateOC.Jumping;
        lane = lane + 1;
        player_sounder.PlayJumpSound();
        jumpTween = transform.DOMoveY(jumpAnimationUpDistance + laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            jumpTween = transform.DOMoveY(laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                player_sounder.PlayLandSound();
                if (state == StateOC.EndGame) return;
                if (state == StateOC.Dead) return;
                state = StateOC.Running;
            });
        });
    }
    public void ArrangeFaoWindStopBool(bool abool)
    {
        FaoWind_StopJump = abool;
        FaoWind_JumpedNew = true;
        FaoWind_JumpedUp = Input.GetKey(moveUp_Button);
    }
    #endregion

    #region Other Functions

    private void Attack()
    {
        player_attack.StartAttack();
    }

    private void SecondAbility()
    {
        if (player.character.characterName == "Fletcher")
        {
            player_sounder.PlaySlideSound();
            player_UI.StartCastTimer(player.character.secondAbilityTimer);
            player_health.Sliding(player.character.secondAbilityTimer);
        }
        if (player.character.characterName == "Woods")
        {
            player_sounder.PlayIgniteSound();
            player_attack.WoodsSecondAbility();
        }
        if (player.character.characterName == "Fao")
        {
            player_attack.FaoSecondAbility();
        }
    }

    public void Die()
    {
        if (animations) player_animation.Death();
        state = StateOC.Dead;
        DeadLocationer = Instantiate(DeadLocation, new Vector3(transform.position.x, laneYPoz[lane], transform.position.z) , Quaternion.identity);
        DeadLocationer.transform.SetParent(platform.transform);
    }
    private void CheckIfDead()
    {
        if (state == StateOC.Dead)
        {
            action = ActionOC.cannot;
        }
    }

    public void EndGame(float movementSpeed, float transitionDuration, float endDuration)
    {
        if (state == StateOC.Dead)
        {
            return;
        }

        action = ActionOC.cannot;
        state = StateOC.EndGame;
        float xPosition = transform.position.x + (movementSpeed / 2) * transitionDuration;
        transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            transform.DOMoveX(transform.position.x + movementSpeed * endDuration, endDuration).SetEase(Ease.Linear);
        });
    }
    #endregion
    #endregion
}
