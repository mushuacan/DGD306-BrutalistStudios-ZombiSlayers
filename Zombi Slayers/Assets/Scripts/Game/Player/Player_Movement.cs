using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Player_Movement : MonoBehaviour
{
    #region Variables

    [Header("BUTTONS")]
    [SerializeField] public KeyCode moveUp_Button;
    [SerializeField] public KeyCode moveDown_Button;
    [SerializeField] private KeyCode moveRight_Button;
    [SerializeField] private KeyCode moveLeft_Button;
    [SerializeField] private KeyCode attack_Button;
    [SerializeField] public KeyCode second_Button;

    [Header("Referances")]
    [SerializeField] private Player_Inputs inputs;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Attack player_attack;
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Health player_health;
    [SerializeField] private Scriptable_PlayerCharacter fixedPlayerCharacter;
    private GameObject platform;

    [Header("Playground Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;

    [Header("Animation Settings")]
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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        StarterPack();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == StateOC.Dead)
            return;
        
        CheckIfDead();
        ArrangeJumping();
        ArrangeMovement();
        CheckButtons();
    }

    #endregion

    #region Movement Functions

    private void StarterPack()
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


        secondAbilityCooldown = 0f;
        FaoWind_StopJump = false;
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
        if ((Input.GetKey(moveUp_Button) || inputs.MovementValues.y > 0.5) && state == StateOC.Running)
        {
            if (lane == 3)
                return;
            else
            {
                JumpBetweenLanes("Up");
            }
        }
        if ((Input.GetKey(moveDown_Button) || inputs.MovementValues.y < -0.5) && state == StateOC.Running)
        {
            if (lane == 1)
                return;
            else
            {
                JumpBetweenLanes("Down");
            }
        }
    }

    private void ArrangeMovement()
    {
        if (action == ActionOC.SecondAbility)
        {
            if (transform.position.x < rightBoundary)
                transform.position = new Vector2(transform.position.x + player.character.secondAbilitySpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            if ((Input.GetKey(moveRight_Button) || inputs.MovementValues.x > 0.5) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x < rightBoundary)
                    transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
            }

            if ((Input.GetKey(moveLeft_Button) || inputs.MovementValues.x < -0.5) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x > leftBoundary)
                    transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
            }
        }
    }
    private void CheckButtons()
    {
        if ((Input.GetKeyDown(attack_Button) || inputs.button0pressed ) && action == ActionOC.Normal)
        {
            Attack();
        }
        if ((Input.GetKeyDown(second_Button) || inputs.button1pressed ) && action == ActionOC.Normal && state != StateOC.Jumping && secondAbilityCooldown < Time.timeSinceLevelLoad)
        {
            action = ActionOC.SecondAbility;

            SecondAbility();

            DOVirtual.DelayedCall(player.character.secondAbilityTimer, () => 
            { 
                action = ActionOC.Normal; 
                secondAbilityCooldown = player.character.secondAbilityCooldown + Time.timeSinceLevelLoad; 
            });
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
        transform.DOMoveY(jumpAnimationUpDistance + transform.position.y, (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOMoveY(laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.InQuad).OnComplete(() =>
            {
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
        transform.DOMoveY(jumpAnimationUpDistance + laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.8f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOMoveY(laneYPoz[lane], (jumpAnimationDuration / animationSpeed) * 0.2f).SetEase(Ease.InQuad).OnComplete(() =>
            {
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
            player_UI.StartCooldown(player.character.secondAbilityTimer);
            player_health.Sliding(player.character.secondAbilityTimer);
        }
        if (player.character.characterName == "Woods")
        {
            player_attack.WoodsSecondAbility();
        }
        if (player.character.characterName == "Fao")
        {
            player_attack.FaoSecondAbility();
        }
    }

    public void Die()
    {
        state = StateOC.Dead;
        transform.SetParent(platform.transform);
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
