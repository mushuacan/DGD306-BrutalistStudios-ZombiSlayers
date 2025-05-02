using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input_v2 : MonoBehaviour
{
    public float PlayerSpeed;
    public int PlayerIndex;

    private PlayerInput _playerInput;

    private InputActionMap _playerMap;

    private InputAction _move;
    private InputAction _fire;
    private InputAction _jump;

    private void Awake()
    {
        PlayerIndex = UnityEngine.Random.Range(0, 128);

        _playerInput = GetComponent<PlayerInput>();

        _playerMap = _playerInput.actions.FindActionMap("Player");

        _move = _playerInput.actions.FindAction("Move");

        _fire = _playerInput.actions.FindAction("Fire");
        _fire.performed += OnFire;

        _jump = _playerInput.actions.FindAction("Jump");
        _jump.performed += OnJump;
    }

    private void Start()
    {

    }

    private void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 inputVector = _move.ReadValue<Vector2>();
        transform.position += new Vector3(inputVector.x, 0f, inputVector.y) * (PlayerSpeed * Time.deltaTime);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log($"Player {PlayerIndex} fired");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Player {PlayerIndex} jumped");
    }

}