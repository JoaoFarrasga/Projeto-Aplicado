using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [HideInInspector] public EnvironmentStateMachine environment;
    [HideInInspector] public CharacterController2D controller;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Inputs input;

    public PlayerState currentState;
    public IdlingState idlingState;
    public WalkingState walkingState;
    public JumpingState jumpingState;
    public AttackingState attackingState;
    public DashingState dashingState;
    public HurtState hurtState;
    public DeathState deathState;

    public string currentStateName;


    private void Awake()
    {
        environment = gameObject.GetComponent<EnvironmentStateMachine>();
        controller = gameObject.GetComponent<CharacterController2D>();
        animator = gameObject.GetComponent<Animator>();
        input = gameObject.GetComponent<Inputs>();

        gameObject.GetComponent<TimeManager>().OnHitAction += OnHitSwitchState;
        gameObject.GetComponent<TimeManager>().OnDeathAction += OnDeathSwitchState;

        idlingState = new IdlingState(this);
        walkingState = new WalkingState(this);
        jumpingState = new JumpingState(this);
        attackingState = new AttackingState(this);
        dashingState = new DashingState(this);
        hurtState = new HurtState(this);
        deathState = new DeathState(this);

        SwitchState(idlingState);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void SwitchState(PlayerState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentStateName = $"{currentState}";
        //Debug.Log($"Current State: {currentState}");
        currentState.OnEnter();
    }

    public void OnHitSwitchState()
    {
        SwitchState(hurtState);
    }

    public void OnDeathSwitchState()
    {
        SwitchState(deathState);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}
