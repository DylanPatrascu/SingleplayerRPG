using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionMapActions
{
    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;
    public bool UnsheathToggle { get; private set; }
    public bool UnSheathPressed { get; private set; } = false;
    public bool GatherPressed { get; private set; }
    public bool AttackPressed { get; private set; }


    [SerializeField] private GameObject Sheath;
    [SerializeField] private GameObject Joint;

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
    }
    private void Update()
    {
        bool isDefaultCombatState =
            _playerState.CurrentPlayerCombatState == PlayerCombatState.Gathering ||
            _playerState.CurrentPlayerCombatState == PlayerCombatState.NotDrawn ||
            _playerState.CurrentPlayerCombatState == PlayerCombatState.Drawn;

        if ((_playerLocomotionInput.MovementInput != Vector2.zero ||
            _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping ||
            _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling) && isDefaultCombatState)
        {
            GatherPressed = false;
            _playerState.SetPlayerCombatState(UnsheathToggle ? PlayerCombatState.Drawn : PlayerCombatState.NotDrawn);
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

    }
    public void ToggleUnsheath()
    {
        UnsheathToggle = !UnsheathToggle;
        _playerState.SetPlayerCombatState(UnsheathToggle ? PlayerCombatState.Drawn : PlayerCombatState.NotDrawn);
    }

    public void ShowUnsheath()
    {
        Sheath.SetActive(!Sheath.activeSelf);
        Joint.SetActive(!Joint.activeSelf);
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
        _playerState.SetPlayerCombatState(PlayerCombatState.Gathering);
    }


    public void SetGatherPressedFalse()
    {
        GatherPressed = false;
        _playerState.SetPlayerCombatState(UnsheathToggle ? PlayerCombatState.Drawn : PlayerCombatState.NotDrawn);

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (_playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping || _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling)
        {
            return;
        }
        if(AttackPressed)
        {
            return;
        }

        if(UnsheathToggle)
        {
            AttackPressed = true;
            _playerState.SetPlayerCombatState(PlayerCombatState.Attacking);
        }
    }

    public void SetAttackPressedFalse()
    {
        AttackPressed = false;
        _playerState.SetPlayerCombatState(PlayerCombatState.Drawn);

        Weapon weapon = Joint.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.ResetEnemiesHit();
        }
    }

}
