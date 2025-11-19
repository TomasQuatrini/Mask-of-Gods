using System.Text.RegularExpressions;
using UnityEngine;

public class StaminaRunningState : IStaminaState
{
    private readonly PlayerStaminaSM _ctx;
    public StaminaRunningState(PlayerStaminaSM ctx) => _ctx = ctx;
    public void Enter() { }
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
            CommandBus.Invoker.Enqueue(
                new SpendStaminaCommand(_ctx.StaminaResource, spend));
            //Debug.Log($"Spending {spend:0.###} stamina in Running State");
        }

        if (_ctx.StaminaResource.Current <= 0f)
            _ctx.ChangeState(_ctx.ExhaustedState);
    }
}