public class TakeDamageCommand : ICommand
{
    private readonly IHealth _health;
    private readonly int _damageAmount;
    public TakeDamageCommand(IHealth health, int damageAmount)
    {
        _health = health;
        _damageAmount = damageAmount;
    }
    public void Execute()
    {
        _health.TakeDamage(_damageAmount);
    }
}