public class RegenStaminaCommand : ICommand
{
    private readonly Resource _stamina;
    private readonly float _amount;

    public RegenStaminaCommand(Resource stamina, float amount)
    {
        _stamina = stamina;
        _amount = amount;
    }

    public void Execute()
    {
        _stamina.Increase(_amount);
    }
}