using UnityEngine;

public class EnemyPatrolState : IEnemyMovementState
{
    private readonly EnemyContext _ctx;
    private readonly EnemyMovementSM _sm;

    private int _currentIndex;
    private float _waitTimer;
    private bool _waiting;

    private const float ArriveThreshold = 0.3f;

    public EnemyPatrolState(EnemyContext ctx, EnemyMovementSM sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        if (_ctx.PatrolPoints == null || _ctx.PatrolPoints.Length == 0)
        {
            // Si no hay patrulla, quedate Idle
            _sm.ToIdle();
            return;
        }

        _ctx.Agent.isStopped = false;

        // Podrías elegir el punto más cercano; por ahora seguimos índice actual
        SetDestinationToCurrentPoint();
        _waiting = false;
        _waitTimer = 0f;
    }

    public void Tick()
    {
        // Si el player entra en radio > Attack
        if (_ctx.IsTargetWithinDetection())
        {
            _sm.ToAttack();
            return;
        }

        if (_ctx.Agent.pathPending) return;

        float remaining = _ctx.Agent.remainingDistance;

        if (!_waiting)
        {
            if (remaining <= ArriveThreshold)
            {
                _waiting = true;
                _waitTimer = 0f;
                _ctx.Agent.isStopped = true;
                // Aquí podrías setear anim de idle de patrulla
            }
        }
        else
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _ctx.PatrolWaitTime)
            {
                _currentIndex = (_currentIndex + 1) % _ctx.PatrolPoints.Length;
                SetDestinationToCurrentPoint();
                _ctx.Agent.isStopped = false;
                _waiting = false;
            }
        }
    }

    public void Exit()
    {
        // Podrías limpiar cosas, pero no es obligatorio
    }

    private void SetDestinationToCurrentPoint()
    {
        if (_ctx.PatrolPoints == null || _ctx.PatrolPoints.Length == 0)
            return;

        Transform targetPoint = _ctx.PatrolPoints[_currentIndex];
        _ctx.Agent.SetDestination(targetPoint.position);
    }
}