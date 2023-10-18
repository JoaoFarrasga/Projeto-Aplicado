using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector] public EnemyController controller;

    public EnemyState currentState;
    public PatrolState patrolState;
    public IdleState idleState;
    public ChaseState chaseState;
    public AttackState attackState;
    public EnemyDeathState deathState;

    public string currentStateName;

    [Header("EnemyStates")]
    public bool hasPatrol;
    public bool hasChase;

    private void Awake()
    {
        controller = gameObject.GetComponent<EnemyController>();

        gameObject.GetComponent<TimeManager>().OnDeathAction += OnDeathSwitchCase;

        patrolState = new PatrolState(this);
        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);
        deathState = new EnemyDeathState(this);

        SwitchState(idleState);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void SwitchState(EnemyState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentStateName = $"{currentState}";
        currentState.OnEnter();
    }

    public void OnDeathSwitchCase()
    {
        SwitchState(deathState);
    }
}
