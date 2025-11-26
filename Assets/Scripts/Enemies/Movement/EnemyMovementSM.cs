using UnityEngine;

public class EnemyMovementSM : MonoBehaviour
{
    private EnemyContext _ctx;
    private IEnemyMovementState _currentState;

    // Estados concretos:
    private EnemyIdleState _idleState;
    private EnemyPatrolState _patrolState;
    private EnemyAttackState _attackState;

    private void Awake()
    {
        _ctx = GetComponentInParent<EnemyContext>();
        if (_ctx == null)
        {
            Debug.LogError("[EnemyMovementSM] Falta EnemyContext");
            enabled = false;
            return;
        }

        _idleState = new EnemyIdleState(_ctx, this);
        _patrolState = new EnemyPatrolState(_ctx, this);
        _attackState = new EnemyAttackState(_ctx, this);
    }

    private void Start()
    {
        // Podés arrancar en Idle o en Patrol
        ChangeState(_patrolState);
    }

    private void Update()
    {
        _currentState?.Tick();
    }

    public void ChangeState(IEnemyMovementState newState)
    {
        if (_currentState == newState) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    // Helpers para los estados:
    public void ToIdle() => ChangeState(_idleState);
    public void ToPatrol() => ChangeState(_patrolState);
    public void ToAttack() => ChangeState(_attackState);
}