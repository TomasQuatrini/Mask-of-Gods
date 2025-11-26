using UnityEngine;

public class StaminaIdleState : IStaminaState
{
    private readonly PlayerStaminaSM _ctx;
    public StaminaIdleState(PlayerStaminaSM ctx) => _ctx = ctx;

    public void Enter()
    {
        Debug.Log("Entering Idle State");
    }
    public void Exit() { }

    public void Tick()
    {
        float regen = _ctx.RegenRateIdle * Time.deltaTime;
        if (regen > 0f) _ctx.StaminaResource.Increase(regen);

        if (_ctx.TryingToRun && _ctx.StaminaResource.Current > 0f)
            _ctx.ChangeState(_ctx.RunningState);
    }
}