using UnityEngine;

public class StaminaExhaustedState : IStaminaState
{
    private readonly PlayerStaminaSM _ctx;
    public StaminaExhaustedState(PlayerStaminaSM ctx) => _ctx = ctx;

    public void Enter()
    {
        
    }
    public void Exit() { }

    public void Tick()
    {
        float regen = _ctx.RegenRateExhausted * Time.deltaTime;
        if (regen > 0f) 
        {
            CommandBus.Invoker.Enqueue(
            new RegenStaminaCommand(_ctx.StaminaResource, regen));
        }
        if (_ctx.RecoveredFromExhausted())
            _ctx.ChangeState(_ctx.IdleState);
    }
}