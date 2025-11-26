using UnityEngine;

public class StaminaExhaustedState : IStaminaState
{
    private readonly PlayerStaminaSM _ctx;
    public StaminaExhaustedState(PlayerStaminaSM ctx) => _ctx = ctx;

    public void Enter()
    {
        Debug.Log("Entering Exhausted State");
    }
    public void Exit() { }

    public void Tick()
    {
        float regen = _ctx.RegenRateExhausted * Time.deltaTime;
        if (regen > 0f) _ctx.StaminaResource.Increase(regen);

        if (_ctx.RecoveredFromExhausted())
            _ctx.ChangeState(_ctx.IdleState);
    }
}