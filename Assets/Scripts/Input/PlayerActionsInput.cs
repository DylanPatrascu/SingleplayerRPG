using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionMapActions
{
    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;
    public bool UnsheathToggle { get; private set; }
    public bool UnSheathPressed { get; private set; }
    public bool GatherPressed { get; private set; }

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
        UnSheathPressed = false;
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

    public void OnUnsheath(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        UnSheathPressed = true;
        Debug.Log("HI");

    }
    public void ToggleUnsheath()
    {
        UnsheathToggle = !UnsheathToggle;
    }
    public void SetUnsheathPressedFalse()
    {
        UnSheathPressed = false;
    }

    public void OnGather(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        GatherPressed = true;
    }


    public void SetGatherPressedFalse()
    {
        GatherPressed = false;
    }

}
