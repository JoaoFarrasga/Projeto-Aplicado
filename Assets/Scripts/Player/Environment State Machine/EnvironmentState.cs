using UnityEngine;

public abstract class EnvironmentState
{
    protected PlayerStateMachine player;
    protected EnvironmentStateMachine environmentStateMachine;
    protected CharacterController2D controller;
    protected Inputs input;
    protected Animator animator;

    public EnvironmentState(EnvironmentStateMachine environment)
    {
        environmentStateMachine = environment;
        player = environment.player;
        controller = player.controller;
        input = player.input;
        animator = player.animator;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();


    protected const string ANIM_PARAM_IS_JUMPING = "IsJumping";
    protected const string ANIM_PARAM_IS_WALL_SLIDING = "IsWallSliding";

    protected const string ANIM_PARAM_JUMP = "Jump";

}

public class WallState : EnvironmentState
{
    public WallState(EnvironmentStateMachine environment) : base(environment) { }

    public override void OnEnter()
    {
        controller.canDash = true;
        controller.doubleJump = true;
        input.jump = false;
        animator.SetBool(ANIM_PARAM_IS_WALL_SLIDING, true);

    }

    public override void OnUpdate()
    {

        if (!controller.walled)
            if (controller.grounded)
                environmentStateMachine.SwitchState(environmentStateMachine.groundedState);
            else
                environmentStateMachine.SwitchState(environmentStateMachine.airboneState);
    }


    public override void OnExit()
    {
        controller.doubleJump = true;
        input.jump = false;
        animator.SetTrigger(ANIM_PARAM_JUMP);
        animator.SetBool(ANIM_PARAM_IS_WALL_SLIDING, false);
    }
}

public class GroundedState : EnvironmentState
{
    public GroundedState(EnvironmentStateMachine environment) : base(environment) { }

    public override void OnEnter()
    {
        controller.canDash = true;
    }

    public override void OnUpdate()
    {


        if (!controller.grounded)
            if (controller.walled)
                environmentStateMachine.SwitchState(environmentStateMachine.wallState);
            else
                environmentStateMachine.SwitchState(environmentStateMachine.airboneState);

    }

    public override void OnExit()
    {

    }
}


public class AirboneState : EnvironmentState
{
    public AirboneState(EnvironmentStateMachine environment) : base(environment) { }


    public override void OnEnter()
    {
        animator.SetBool(ANIM_PARAM_IS_JUMPING, true);
    }

    public override void OnUpdate()
    {


        if (controller.m_Rigidbody2D.velocity.y < -controller.maxFallSpeed)
            controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, -controller.maxFallSpeed);


        if (controller.grounded)
            environmentStateMachine.SwitchState(environmentStateMachine.groundedState);
        else if (controller.walled)
            environmentStateMachine.SwitchState(environmentStateMachine.wallState);
    }

    public override void OnExit()
    {
        animator.SetBool(ANIM_PARAM_IS_JUMPING, false);
    }
}
