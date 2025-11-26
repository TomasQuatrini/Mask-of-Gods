using UnityEngine;

public class StaminaRunningState : IStaminaState
{
    private readonly PlayerStaminaSM _ctx;
    public StaminaRunningState(PlayerStaminaSM ctx) => _ctx = ctx;

    public void Enter()
    { 
        Debug.Log("Entering Running State");
    }
    public void Exit() { }

    public void Tick()
    {
        if (!_ctx.TryingToRun)
        {
            _ctx.ChangeState(_ctx.IdleState);
            return;
        }

        float spend = _ctx.SpendRateRunning * Time.deltaTime;
        if (spend > 0f)
        {
            bool ok = _ctx.StaminaResource.Spend(spend);
            if (!ok && _ctx.StaminaResource.Current > 0f)
                _ctx.StaminaResource.Spend(_ctx.StaminaResource.Current);
        }

        if (_ctx.StaminaResource.Current <= 0f)
            _ctx.ChangeState(_ctx.ExhaustedState);
    }
}