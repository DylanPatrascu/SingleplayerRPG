using System.Linq;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float locomotionBlendSpeed = 4f;

    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;
    private PlayerController _playerController;
    private PlayerActionsInput _playerActionsInput;

    private static int inputXHash = Animator.StringToHash("InputX");
    private static int inputYHash = Animator.StringToHash("InputY");
    private static int inputMagnitudeHash = Animator.StringToHash("InputMagnitude");
    private static int isIdlingHash = Animator.StringToHash("IsIdling");
    private static int isGroundedHash = Animator.StringToHash("IsGrounded");
    private static int isFallingHash = Animator.StringToHash("IsFalling");
    private static int isJumpingHash = Animator.StringToHash("IsJumping");
    
    // Actions
    private static int isUnsheathingHash = Animator.StringToHash("IsUnsheathing");
    private static int isSheathedHash = Animator.StringToHash("IsUnsheathed");
    private static int isAttackingHash = Animator.StringToHash("IsAttacking");


    private static int isGatheringHash = Animator.StringToHash("IsGathering");
    private static int isPlayingActionHash = Animator.StringToHash("IsPlayingAction");
    private int[] actionHashes;

    private static int isRotatingToTargetHash = Animator.StringToHash("IsRotatingToTarget");
    private static int rotationMismatchHash = Animator.StringToHash("RotationMismatch");

    private Vector3 _currentBlendInput = Vector3.zero;

    private float _sprintMaxBlendValue = 1.5f;
    private float _runMaxBlendValue = 1f;
    private float _walkMaxBlendValue = 0.5f;


    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
        _playerController = GetComponent<PlayerController>();
        _playerActionsInput = GetComponent<PlayerActionsInput>();

        // Interruptable
        actionHashes = new int[] { isGatheringHash, isAttackingHash };
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
        bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
        bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
        bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
        bool isGrounded = _playerState.InGroundedState();
        bool isPlayingAction = actionHashes.Any(hash => _animator.GetBool(hash));

        bool isRunBlendValue = isRunning || isJumping || isFalling;
        Vector2 inputTarget = isSprinting ? _playerLocomotionInput.MovementInput * _sprintMaxBlendValue : isRunBlendValue ? _playerLocomotionInput.MovementInput * _runMaxBlendValue : _playerLocomotionInput.MovementInput * _walkMaxBlendValue;
        _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);

        _animator.SetBool(isIdlingHash, isIdling);
        _animator.SetBool(isGroundedHash, isGrounded);
        _animator.SetBool(isFallingHash, isFalling);
        _animator.SetBool(isJumpingHash, isJumping);

        _animator.SetBool(isUnsheathingHash, _playerActionsInput.UnSheathPressed);
        _animator.SetBool(isSheathedHash, _playerActionsInput.UnsheathToggle);
        _animator.SetBool(isAttackingHash, _playerActionsInput.AttackPressed);

        _animator.SetBool(isGatheringHash, _playerActionsInput.GatherPressed);
        _animator.SetBool(isPlayingActionHash, isPlayingAction);

        _animator.SetBool(isRotatingToTargetHash, _playerController.IsRotatingToTarget);
        _animator.SetFloat(inputXHash, _currentBlendInput.x);
        _animator.SetFloat(inputYHash, _currentBlendInput.y);
        _animator.SetFloat(inputMagnitudeHash, _currentBlendInput.magnitude);
        _animator.SetFloat(rotationMismatchHash, _playerController.RotationMismatch);
    }

}
