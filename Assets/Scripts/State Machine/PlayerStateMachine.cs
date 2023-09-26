using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterController2D controller;
    public Inputs input;
    public Animator animator;

    private BaseState currentState;
    public IdlingState idlingState;
    public WalkingState walkingState;
    public JumpingState jumpingState;
    public AttackingState attackingState;
    public DashingState dashingState;
    public HitState hitState;

    public string currentStateName;


    private void Awake()
    {
        gameObject.GetComponent<TimeManager>().OnHit += OnHitSwitchState;

        idlingState = new IdlingState(this);
        walkingState = new WalkingState(this);
        jumpingState = new JumpingState(this);
        attackingState = new AttackingState(this);
        dashingState = new DashingState(this);
        hitState = new HitState(this);

        SwitchState(idlingState);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void SwitchState(BaseState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentStateName = $"{currentState}";
        //Debug.Log($"Current State: {currentState}");
        currentState.OnEnter();
    }

    public void OnHitSwitchState()
    {
        SwitchState(hitState);
    }
}
