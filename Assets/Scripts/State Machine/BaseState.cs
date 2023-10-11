using System.Collections;
using UnityEngine;
using UnityEngine.Windows;


public abstract class BaseState
{
    protected PlayerStateMachine player;

    public BaseState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

}

public class WalkingState : BaseState
{
    public WalkingState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        player.controller.Move(player.input.move.x * Time.deltaTime, false, false);

        player.animator.SetFloat("Speed", Mathf.Abs(player.input.move.x * player.controller.runSpeed * Time.deltaTime));



        if (Mathf.Abs(player.input.move.x) <= 0.1f)
            player.SwitchState(player.idlingState);

        if (player.input.jump)
            player.SwitchState(player.jumpingState);
        if (player.input.primaryAttack)
            player.SwitchState(player.attackingState);
        if (player.input.dash)
            player.SwitchState(player.dashingState);

    }


    public override void OnExit()
    {
        player.animator.SetFloat("Speed", Mathf.Abs(0));
    }
}

public class IdlingState : BaseState
{
    public IdlingState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        player.animator.SetFloat("Speed", 0);


        if (Mathf.Abs(player.input.move.x) > 0.1f)
            player.SwitchState(player.walkingState);

        if (player.input.jump)
            player.SwitchState(player.jumpingState);
        if (player.input.primaryAttack)
            player.SwitchState(player.attackingState);
        if (player.input.dash)
            player.SwitchState(player.dashingState);
    }

    public override void OnExit()
    {

    }
}


public class JumpingState : BaseState
{
    public JumpingState(PlayerStateMachine player) : base(player) { }


    public override void OnEnter()
    {
        player.animator.SetBool("IsJumping", true);
        player.input.jump = false;
        player.controller.Move(player.input.move.x * Time.deltaTime, true, false);
    }

    public override void OnUpdate()
    {
        player.controller.Move(player.input.move.x * Time.deltaTime, false, false);


        if (player.controller.m_Grounded && Mathf.Abs(player.input.move.x) > 0.1f)
            player.SwitchState(player.walkingState);
        else if (player.controller.m_Grounded)
            player.SwitchState(player.idlingState);
        if (player.input.primaryAttack)
            player.SwitchState(player.attackingState);
        if (player.input.dash)
            player.SwitchState(player.dashingState);
    }

    public override void OnExit()
    {
        player.animator.SetBool("IsJumping", false);
    }
}

public class AttackingState : BaseState
{
    public AttackingState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;

    public override void OnEnter()
    {
        timePassed = 0f;
        player.controller.Move(0, false, false);

        player.animator.SetBool("IsAttacking", true);
        player.animator.SetBool("CanWalk", false);
        if (player.input.primaryAttack)
        {
            player.input.primaryAttack = false;
        }
        if (player.input.secondaryAttack)
        {
            player.input.secondaryAttack = false;

        }
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed > 0.3f)
            player.SwitchState(player.idlingState);


    }

    public override void OnExit()
    {
        player.animator.SetBool("IsAttacking", false);
        player.animator.SetBool("CanWalk", true);
    }
}


public class HitState : BaseState
{
    public HitState(PlayerStateMachine player) : base(player) { }

    float timePassed = 0f;

    public override void OnEnter()
    {
        player.animator.SetTrigger("Hit");
        player.animator.SetBool("CanWalk", false);
        timePassed = 0f;
    }

    public override void OnUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed > player.controller.hitTimeout)
            player.SwitchState(player.idlingState);
    }

    public override void OnExit()
    {
        player.animator.SetBool("CanWalk", true);
    }
}

public class DashingState : BaseState
{
    public DashingState(PlayerStateMachine player) : base(player) { }


    public override void OnEnter()
    {
        player.input.dash = false;
        player.controller.Move(player.input.move.x * Time.deltaTime, false, true);
    }

    public override void OnUpdate()
    {

        if (Mathf.Abs(player.input.move.x) > 0.1f)
            player.SwitchState(player.walkingState);
        if (Mathf.Abs(player.input.move.x) <= 0.1f)
            player.SwitchState(player.idlingState);
        if (player.input.jump)
            player.SwitchState(player.jumpingState);
        if (player.input.primaryAttack)
            player.SwitchState(player.attackingState);
    }

    public override void OnExit()
    {

    }
}

