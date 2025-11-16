using UnityEngine;

public class HealCommand : ICommand
{
    private readonly IHealth _health;
    private readonly int _healAmount;

    public HealCommand(IHealth health, int healAmount)
    {
        _health = health;
        _healAmount = healAmount;
    }
    public void Execute()
    {
        _health.Heal(_healAmount);
    }
}
