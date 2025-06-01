using UnityEngine;

public enum PlayerMovementState
{
    Idling = 0,
    Walking = 1,
    Running = 2,
    Sprinting = 3,
    Jumping = 4,
    Falling = 5,
    Strafing = 6,
}

public enum PlayerCombatState
{
    NotDrawn = 0,
    Drawn = 1,
    Attacking = 2,
    Dodging = 3,
    Blocking = 4,
    Casting = 5,
    Healing = 6,
    Dead = 7,
    Gathering = 8,
}

public class PlayerState : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; private set; } = PlayerMovementState.Idling;
    [field: SerializeField] public PlayerCombatState CurrentPlayerCombatState { get; private set; } = PlayerCombatState.NotDrawn;

    public void SetPlayerMovementState(PlayerMovementState playerMovementState)
    {
        CurrentPlayerMovementState = playerMovementState;
    }

    public void SetPlayerCombatState(PlayerCombatState playerCombatState)
    {
        CurrentPlayerCombatState = playerCombatState;
    }

    public bool InGroundedState()
    {
        return IsStateGroundedState(CurrentPlayerMovementState);
    }

    public bool IsStateGroundedState(PlayerMovementState movementState)
    {
        return movementState == PlayerMovementState.Idling || movementState == PlayerMovementState.Walking || movementState == PlayerMovementState.Running || movementState == PlayerMovementState.Sprinting;
    }
}
