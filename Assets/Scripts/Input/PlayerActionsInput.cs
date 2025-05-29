using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionMapActions
{
    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;

    private float _comboTimer = 1f;
    private float _lastAttackTime = -Mathf.Infinity;

    public bool AttackCombo { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool GatherPressed { get; private set; }

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
    }
    private void Update()
    {
        if (_playerLocomotionInput.MovementInput != Vector2.zero || _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping || _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling)
        {
            GatherPressed = false;
        }
    }
    private void OnEnable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized, cannot enable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerActionMap.Enable();
        PlayerInputManager.Instance.PlayerControls.PlayerActionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized, cannot disable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerActionMap.Disable();
        PlayerInputManager.Instance.PlayerControls.PlayerActionMap.RemoveCallbacks(this);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }

        AttackPressed = true;

        float currentTime = Time.time;

        if (currentTime - _lastAttackTime <= _comboTimer)
        {
            AttackCombo = true;
            print("ATTACK COMBO!!!");
        }
        else
        {
            AttackCombo = false;
        }
        _lastAttackTime = currentTime;
    }

    public void OnGather(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        GatherPressed = true;
    }

    public void SetAttackPressedFalse()
    {
        AttackPressed = false;
    }
    public void ResetAttackCombo()
    {
        AttackCombo = false;
    }

    public void SetGatherPressedFalse()
    {
        GatherPressed = false;
    }
}
