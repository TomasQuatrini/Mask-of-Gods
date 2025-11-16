public class SpendStaminaCommand : ICommand
{
    private readonly Resource _stamina;
    private readonly float _amount;

    public SpendStaminaCommand(Resource stamina, float amount)
    {
        _stamina = stamina;
        _amount = amount;
    }

    public void Execute()
    {
        _stamina.Spend(_amount);
    }
}