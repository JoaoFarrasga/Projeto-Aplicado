using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool primaryAttack;
    public bool secondaryAttack;
    public bool dash;
    public bool grapple;
    public bool pause;
    public bool interact;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {

        JumpInput(value.isPressed);
    }

    public void OnPrimaryAttack(InputValue value)
    {
        PrimaryAttackInput(value.isPressed);
    }

    public void OnSecondaryAttack(InputValue value)
    {
        SecondaryAttackInput(value.isPressed);
    }

    public void OnDash(InputValue value)
    {
        DashInput(value.isPressed);
    }

    public void OnGrapple(InputValue value)
    {
        GrappleInput(value.Get<float>() > 0.5f);
    }
    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }

    public void OnPause(InputValue value)
    {
        GameManager.instance.PauseMenu();
    }


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void PrimaryAttackInput(bool newPrimaryAttackState)
    {
        primaryAttack = newPrimaryAttackState;
    }

    public void SecondaryAttackInput(bool newSecondaryAttackState)
    {
        secondaryAttack = newSecondaryAttackState;
    }

    public void DashInput(bool newDashState)
    {
        dash = newDashState;
    }

    public void GrappleInput(bool newGrappleState)
    {
        grapple = newGrappleState;
    }
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

