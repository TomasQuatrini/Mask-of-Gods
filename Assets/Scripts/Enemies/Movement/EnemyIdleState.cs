using UnityEngine;

public class EnemyIdleState : IEnemyMovementState
{
    private readonly EnemyContext _ctx;
    private readonly EnemyMovementSM _sm;

    private float _timer;
    private const float IdleDuration = 2f; // lo podés parametrizar

    public EnemyIdleState(EnemyContext ctx, EnemyMovementSM sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.Agent.isStopped = true;
        _timer = 0f;
        // Podrías setear animación Idle acá
    }

    public void Tick()
    {
        // Si el player entra en rango > ir a Attack
        if (_ctx.IsTargetWithinDetection())
        {
            _sm.ToAttack();
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= IdleDuration)
        {
            // Cuando termine de estar idle, vuelve a patrullar
            _sm.ToPatrol();
        }
    }

    public void Exit()
    {
        // Nada en particular
    }
}