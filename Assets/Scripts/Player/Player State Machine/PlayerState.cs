using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using System;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected EnvironmentStateMachine environment;
    protected CharacterController2D controller;
    protected Inputs input;
    protected Animator animator;

    public PlayerState(PlayerStateMachine player)
    {
        playerStateMachine = player;
        environment = player.environment;
        controller = player.controller;
        input = player.input;
        animator = player.animator;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    protected const string ANIM_PARAM_SPEED = "Speed";
    protected const string ANIM_PARAM_ATTACK_SPEED = "AttackSpeed";

    protected const string ANIM_PARAM_IS_JUMPING = "IsJumping";
    protected const string ANIM_PARAM_IS_WALL_SLIDING = "IsWallSliding";
    protected const string ANIM_PARAM_IS_DEAD = "IsDead";

    protected const string ANIM_PARAM_JUMP = "Jump";
    protected const string ANIM_PARAM_DOUBLE_JUMP = "DoubleJump";
    protected const string ANIM_PARAM_DASH = "Dash";
    protected const string ANIM_PARAM_ATTACK = "Attack";
    protected const string ANIM_PARAM_HIT = "Hit";

}

public class WalkingState : PlayerState
{
    public WalkingState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        controller.Move(input.move.x);
        animator.SetFloat(ANIM_PARAM_SPEED, Mathf.Abs(input.move.x * controller.speed * Time.deltaTime));

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        //else if (input.dash && controller.canDash)
        //    playerStateMachine.SwitchState(playerStateMachine.dashingState);
        else if (input.grapple && controller.canGrapple)
            playerStateMachine.SwitchState(playerStateMachine.grapplingState);
        else if (input.interact)
            playerStateMachine.SwitchState(playerStateMachine.interactingState);
        else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
            playerStateMachine.SwitchState(playerStateMachine.jumpingState);
        else if (environment.isWallState && input.move.x * controller.transform.localScale.x > controller.deadZone || Mathf.Abs(input.move.x) <= controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.idlingState);
    }


    public override void OnExit()
    {
        animator.SetFloat(ANIM_PARAM_SPEED, 0);
    }
}

public class IdlingState : PlayerState
{
    public IdlingState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (environment.isWallState && input.move.x * controller.transform.localScale.x > controller.deadZone)
            controller.rigidBody.velocity = new Vector3(0, 0);

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        //else if (input.dash && controller.canDash)
        //    playerStateMachine.SwitchState(playerStateMachine.dashingState);
        else if (input.grapple && controller.canGrapple)
            playerStateMachine.SwitchState(playerStateMachine.grapplingState);
        else if (input.interact)
            playerStateMachine.SwitchState(playerStateMachine.interactingState);
        else if (input.jump)
        {
            if (controller.grounded || controller.doubleJump || environment.isWallState && input.move.x * controller.transform.localScale.x > controller.deadZone)
                playerStateMachine.SwitchState(playerStateMachine.jumpingState);
            else
                input.jump = false;
        }
        else if (Mathf.Abs(input.move.x) > controller.deadZone && !environment.isWallState || environment.isWallState && input.move.x * controller.transform.localScale.x < controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.walkingState);


    }

    public override void OnExit()
    {

    }
}

public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;


    public override void OnEnter()
    {
        if (!controller.grounded)
            controller.doubleJump = false;



        controller.Jump(isDoubleJump: !controller.doubleJump,
            isWallJump: environment.isWallState && input.move.x * controller.transform.localScale.x > controller.deadZone);

        timePassed = 0f;

        if (controller.doubleJump)
            input.jump = false;
        animator.SetTrigger(ANIM_PARAM_JUMP);
    }

    public override void OnUpdate()
    {
        controller.Move(input.move.x);
        if (!controller.doubleJump)
            input.jump = false;

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        //else if (input.dash && controller.canDash)
        //    playerStateMachine.SwitchState(playerStateMachine.dashingState);
        else if (input.grapple && controller.canGrapple)
            playerStateMachine.SwitchState(playerStateMachine.grapplingState);
        else if (input.jump)
        {
            playerStateMachine.SwitchState(playerStateMachine.jumpingState);
            animator.SetTrigger(ANIM_PARAM_DOUBLE_JUMP);
        }

        timePassed += Time.deltaTime;

        if (timePassed > controller.jumpTimeout)
            if (!environment.isAirboneState)
                if (Mathf.Abs(input.move.x) > controller.deadZone)
                    playerStateMachine.SwitchState(playerStateMachine.walkingState);
                else
                    playerStateMachine.SwitchState(playerStateMachine.idlingState);

    }

    public override void OnExit()
    {

    }
}

public class DashingState : PlayerState
{
    public DashingState(PlayerStateMachine player) : base(player) { }
    float timePassed = 0f;


    public override void OnEnter()
    {
        timePassed = 0f;
        controller.Move(input.move.x);
        controller.Dash();
        animator.SetTrigger(ANIM_PARAM_DASH);
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;
        controller.Move(input.move.x);

        if (timePassed < controller.dashTimeout)
            return;

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
            playerStateMachine.SwitchState(playerStateMachine.jumpingState);
        else if (Mathf.Abs(input.move.x) > controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.walkingState);
        else if (Mathf.Abs(input.move.x) <= controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.idlingState);
    }

    public override void OnExit()
    {
        if (!environment.isAirboneState)
            controller.canDash = true;
        controller.rigidBody.velocity = Vector3.zero;
        input.dash = false;
    }
}

public class GrapplingState : PlayerState
{
    public GrapplingState(PlayerStateMachine player) : base(player) { }
    float timePassed = 0f;
    Vector2 targetPosition;

    // Add a field for the LineRenderer
    LineRenderer grappleLineRenderer;

    public override void OnEnter()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        // Calculate the direction from player to mouse cursor
        Vector2 direction = (mousePosition - (Vector2)controller.transform.position).normalized;

        // Perform a raycast in the direction of the mouse cursor
        RaycastHit2D hit = Physics2D.Raycast((Vector2)controller.transform.position, direction, controller.grappleRange, controller.groundLayer);

        if (hit.collider != null)
        {
            // If the ray hits something, set the hit point as the target position
            targetPosition = hit.point;

            // Create or enable the LineRenderer
            if (grappleLineRenderer == null)
            {
                grappleLineRenderer = controller.gameObject.AddComponent<LineRenderer>();
                // Set LineRenderer properties (adjust as needed)
                grappleLineRenderer.startWidth = 0.1f;
                grappleLineRenderer.endWidth = 0.1f;
                //grappleLineRenderer.material = new Material(Shader.Find("Sprites/GrappleLine"));
                grappleLineRenderer.startColor = Color.white;
                grappleLineRenderer.endColor = Color.white;
            }

            // Set the LineRenderer positions
            grappleLineRenderer.positionCount = 2;
            grappleLineRenderer.SetPosition(0, controller.transform.position);
            grappleLineRenderer.SetPosition(1, targetPosition);
        }
        else
        {
            // If the ray doesn't hit anything, switch to the idling state
            playerStateMachine.SwitchState(playerStateMachine.idlingState);
            return;
        }

        timePassed = 0f;
        animator.SetTrigger(ANIM_PARAM_JUMP);
        controller.canGrapple = false;
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;

        if (!controller.canGrapple)
            controller.GrapplePull(targetPosition);

        // Update the LineRenderer positions
        if (grappleLineRenderer != null)
        {
            grappleLineRenderer.SetPosition(0, controller.transform.position);
            grappleLineRenderer.SetPosition(1, targetPosition);
        }

        if (timePassed < controller.grappleTimeout)
            return;

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
            playerStateMachine.SwitchState(playerStateMachine.jumpingState);
        else if (Mathf.Abs(input.move.x) > controller.deadZone && !input.grapple)
            playerStateMachine.SwitchState(playerStateMachine.walkingState);
        else if (Mathf.Abs(input.move.x) <= controller.deadZone && !input.grapple)
            playerStateMachine.SwitchState(playerStateMachine.idlingState);
    }

    public override void OnExit()
    {
        // Remove the LineRenderer component when exiting the grapple state
        if (grappleLineRenderer != null)
        {
            MonoBehaviour.Destroy(grappleLineRenderer);
            grappleLineRenderer = null;
        }

        controller.rigidBody.velocity = Vector3.zero;
        if (!controller.canGrapple)
            controller.StartGrappleCooldown();
    }
}

    
public class InteractingState : PlayerState
{
    public InteractingState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        //animator.SetTrigger(ANIM_PARAM_DASH);
        controller.Interact();
    }

    public override void OnUpdate()
    {


        controller.Move(input.move.x);

        if (input.primaryAttack || input.secondaryAttack)
            playerStateMachine.SwitchState(playerStateMachine.attackingState);
        else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
            playerStateMachine.SwitchState(playerStateMachine.jumpingState);
        else if (Mathf.Abs(input.move.x) > controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.walkingState);
        else if (Mathf.Abs(input.move.x) <= controller.deadZone)
            playerStateMachine.SwitchState(playerStateMachine.idlingState);
    }

    public override void OnExit()
    {
       input.interact = false;
        
    }
}


public class AttackingState : PlayerState
{
    public AttackingState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;
    float currentAttackSpeed = 0f;
    float currentAttackTimeout = 0f;
    float currentAttackDamage = 0f;

    public override void OnEnter()
    {
        timePassed = 0f;
        if (environment.isAirboneState)
            controller.rigidBody.velocity = new Vector3(0, 0);
        if (input.primaryAttack)
        {
            currentAttackTimeout = controller.primaryAttackTimeout;
            currentAttackSpeed = controller.primaryAttackSpeed;
            currentAttackDamage = controller.primaryAttackDamage;
        }
        else
        {
            currentAttackTimeout = controller.secondaryAttackTimeout;
            currentAttackSpeed = controller.secondaryAttackSpeed;
            currentAttackDamage = controller.secondaryAttackDamage;
        }


        animator.SetTrigger(ANIM_PARAM_ATTACK);
        animator.SetFloat(ANIM_PARAM_ATTACK_SPEED, currentAttackSpeed);

        if (environment.isAirboneState)
            controller.rigidBody.velocity = new Vector2(controller.rigidBody.velocity.x, controller.attackGravityCancel);

        controller.Attack(currentAttackDamage);
    }

    public override void OnUpdate()
    {

        timePassed += Time.deltaTime;

        if (timePassed > currentAttackTimeout)
            if (input.grapple && controller.canGrapple)
                playerStateMachine.SwitchState(playerStateMachine.grapplingState);
            //else if (input.dash && controller.canDash)
            //    playerStateMachine.SwitchState(playerStateMachine.dashingState);
            else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
                playerStateMachine.SwitchState(playerStateMachine.jumpingState);
            else if (Mathf.Abs(input.move.x) > controller.deadZone)
                playerStateMachine.SwitchState(playerStateMachine.walkingState);
            else if (Mathf.Abs(input.move.x) <= controller.deadZone)
                playerStateMachine.SwitchState(playerStateMachine.idlingState);
    }

    public override void OnExit()
    {
        input.primaryAttack = false;
        input.secondaryAttack = false;
    }
}


public class HurtState : PlayerState
{
    public HurtState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;

    public override void OnEnter()
    {
        animator.SetTrigger(ANIM_PARAM_HIT);
        timePassed = 0f;
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed > controller.hurtTimeout)
            if (input.primaryAttack || input.secondaryAttack)
                playerStateMachine.SwitchState(playerStateMachine.attackingState);
            //else if (input.dash && controller.canDash)
            //    playerStateMachine.SwitchState(playerStateMachine.dashingState);
            else if (input.grapple && controller.canGrapple)
                playerStateMachine.SwitchState(playerStateMachine.grapplingState);
            else if (input.jump && controller.grounded || input.jump && controller.doubleJump)
                playerStateMachine.SwitchState(playerStateMachine.jumpingState);
            else if (Mathf.Abs(input.move.x) > controller.deadZone)
                playerStateMachine.SwitchState(playerStateMachine.walkingState);
            else if (Mathf.Abs(input.move.x) <= controller.deadZone)
                playerStateMachine.SwitchState(playerStateMachine.idlingState);

    }

    public override void OnExit()
    {

    }
}

public class DeathState : PlayerState
{
    public DeathState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;

    public override void OnEnter()
    {
        animator.SetBool(ANIM_PARAM_IS_DEAD, true);
        timePassed = 0f;
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed > controller.deathTimeout)
            GameManager.instance.DeathMenu();

    }

    public override void OnExit()
    {

    }
}