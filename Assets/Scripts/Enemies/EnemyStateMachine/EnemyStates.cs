using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyController enemyController;

    public EnemyState(EnemyStateMachine enemy)
    {
        enemyStateMachine = enemy;
        enemyController = enemy.controller;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

public class PatrolState : EnemyState
{
    public PatrolState(EnemyStateMachine enemy) : base(enemy) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        enemyController.Patrol();

        if(enemyStateMachine.hasChase && enemyController.seePlayer)
            enemyStateMachine.SwitchState(enemyStateMachine.chaseState);
    }

    public override void OnExit()
    {

    }
}

public class IdleState : EnemyState
{
    public IdleState(EnemyStateMachine enemy) : base(enemy) { }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (enemyStateMachine.hasChase && enemyController.seePlayer)
                enemyStateMachine.SwitchState(enemyStateMachine.chaseState);
        else if (enemyStateMachine.hasPatrol)
            enemyStateMachine.SwitchState(enemyStateMachine.patrolState);
    }

    public override void OnExit()
    {

    }
}

public class ChaseState : EnemyState
{
    public ChaseState(EnemyStateMachine enemy) : base(enemy) { }
    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        enemyController.Chase();

        if (!enemyController.seePlayer)
            enemyStateMachine.SwitchState(enemyStateMachine.idleState);
    
        if (Physics2D.OverlapCircle(enemyController.gameObject.transform.position, 1.0f, enemyController.playerLayer))
            enemyStateMachine.SwitchState(enemyStateMachine.attackState);
    }

    public override void OnExit()
    {

    }
}

public class AttackState : EnemyState
{
    public AttackState(EnemyStateMachine enemy) : base(enemy) { }

    float timeSinceAttack = 0f;
    float attackTimeOut = 0f;

    public override void OnEnter()
    {
        timeSinceAttack = 0f;

        enemyController.Attack();

        attackTimeOut = enemyController.attackTimeOut;
    }

    public override void OnUpdate()
    {
        timeSinceAttack += Time.deltaTime;

        if (timeSinceAttack > attackTimeOut)
            enemyStateMachine.SwitchState(enemyStateMachine.idleState);
    }

    public override void OnExit()
    {

    }
}

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyStateMachine enemy) : base(enemy) { }

    float timeSinceDeath = 0f;

    public override void OnEnter()
    {
        timeSinceDeath = 0f;
    }

    public override void OnUpdate()
    {
        timeSinceDeath += Time.deltaTime;

        if (timeSinceDeath > enemyController.deathTimeOut)
            Object.Destroy(enemyController.gameObject);
    }

    public override void OnExit()
    {

    }
}

