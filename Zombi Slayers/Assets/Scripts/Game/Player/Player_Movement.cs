using UnityEngine;
using DG.Tweening;

public class Player_Movement : MonoBehaviour
{
    #region Variables

    [Header("BUTTONS")]
    [SerializeField] private KeyCode moveUp_Button;
    [SerializeField] private KeyCode moveDown_Button;
    [SerializeField] private KeyCode moveRight_Button;
    [SerializeField] private KeyCode moveLeft_Button;
    [SerializeField] private KeyCode attack_Button;
    [SerializeField] private KeyCode second_Button;

    [Header("Referances")]
    [Tooltip("Haritayý hareket ettiren objeyi baðlayýnýz. (Halihazýrdaki adý KayanObje)")]
    [SerializeField] private GameObject platform;
    [SerializeField] private Player_Character player;
    [SerializeField] private Player_Attack player_attack;
    [SerializeField] private Player_UI player_UI;
    [SerializeField] private Player_Health player_health;

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
    [SerializeField] [Range(1, 3)] private int lane;
    private float[] laneYPoz = { -1f, -3f, 0.25f, 3.5f };

    [Header("(private variables)")]
    [SerializeField] public StateOC state;
    [SerializeField] public ActionOC action;
    private float secondAbilityCooldown;

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

    void Start()
    {
        state = StateOC.Running;
        action = ActionOC.Normal;

        laneYPoz = LaneFinder.laneYPositions;

        transform.position = new Vector2(startPositionX, laneYPoz[lane]);

        if (platform == null)
        {
            Debug.LogError(gameObject + " objesinde platform için referans ayarlanmamýþ.");
            Time.timeScale = 0f;
        }

        secondAbilityCooldown = 0f;
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

    private void ArrangeJumping()
    {
        if (Input.GetKey(moveUp_Button) && state == StateOC.Running)
        {
            if (lane == 3)
                return;
            else
            {
                state = StateOC.Jumping;
                lane = lane + 1;
                JumpBetweenLanes("Up");
            }
        }
        if (Input.GetKey(moveDown_Button) && state == StateOC.Running)
        {
            if (lane == 1)
                return;
            else
            {
                state = StateOC.Jumping;
                lane = lane - 1;
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
            if (Input.GetKey(moveRight_Button) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x < rightBoundary)
                    transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
            }

            if (Input.GetKey(moveLeft_Button) && (state == StateOC.Running || state == StateOC.Jumping))
            {
                if (transform.position.x > leftBoundary)
                    transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
            }
        }
    }
    private void CheckButtons()
    {
        if (Input.GetKeyDown(attack_Button) && action == ActionOC.Normal)
        {
            Attack();
        }
        if (Input.GetKeyDown(second_Button) && action == ActionOC.Normal && secondAbilityCooldown < Time.timeSinceLevelLoad)
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
        if (Vertical == "Up")
        {
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
