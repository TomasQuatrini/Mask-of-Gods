using UnityEngine;

public class EnemyAttackState : IEnemyMovementState
{
    private readonly EnemyContext _ctx;
    private readonly EnemyMovementSM _sm;

    private const float LoseSightExtra = 2f; // histéresis para no cambiar de estado todo el tiempo

    public EnemyAttackState(EnemyContext ctx, EnemyMovementSM sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        if (_ctx.Target == null)
        {
            // Si por algún motivo no tiene player, vuelve a patrulla
            _sm.ToPatrol();
            return;
        }

        _ctx.Agent.isStopped = false;
        // Animación de correr / atacar
    }

    public void Tick()
    {
        if (_ctx.Target == null)
        {
            _sm.ToPatrol();
            return;
        }

        float dist = _ctx.DistanceToTarget();

        // Si el player se fue demasiado lejos, volver a patrulla
        if (dist > _ctx.DetectionRadius + LoseSightExtra)
        {
            _sm.ToPatrol();
            return;
        }

        // Si está dentro del rango de ataque > aquí iría tu lógica de pegar/disparar
        if (_ctx.IsTargetWithinAttackRange())
        {
            _ctx.Agent.isStopped = true;

            // Rotar hacia el jugador
            Vector3 dir = _ctx.Target.position - _ctx.Transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                _ctx.Transform.rotation = Quaternion.Slerp(
                    _ctx.Transform.rotation,
                    targetRot,
                    10f * Time.deltaTime
                );
            }
            Debug.Log("Ejecutando Logica de ataque");
            _ctx.ComponentAttack.Attack();
        }
        else
        {
            // Todavía no está en rango: seguir persiguiendo
            _ctx.Agent.isStopped = false;
            _ctx.Agent.SetDestination(_ctx.Target.position);
        }
    }

    public void Exit()
    {
        _ctx.Agent.isStopped = false;
    }
}