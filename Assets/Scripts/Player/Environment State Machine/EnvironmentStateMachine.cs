using UnityEngine;

public class EnvironmentStateMachine : MonoBehaviour
{
    [HideInInspector] public PlayerStateMachine player;

    public EnvironmentState currentState;
    public GroundedState groundedState;
    public AirboneState airboneState;
    public WallState wallState;

    public bool isGroundedState { get { return currentState == groundedState; } }
    public bool isAirboneState { get { return currentState == airboneState; } }
    public bool isWallState { get { return currentState == wallState; } }


    public string currentStateName;


    private void Awake()
    {
        player = GetComponent<PlayerStateMachine>();

        groundedState = new GroundedState(this);
        airboneState = new AirboneState(this);
        wallState = new WallState(this);

        SwitchState(groundedState);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void SwitchState(EnvironmentState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentStateName = $"{currentState}";
        //Debug.Log($"Current State: {currentState}");
        currentState.OnEnter();
    }
}
