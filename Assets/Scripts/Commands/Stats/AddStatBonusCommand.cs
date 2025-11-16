using Game.Stats;

public class AddStatBonusCommand : ICommand
{
    private readonly Stat _stat;
    private readonly float _bonusAmount;
    private readonly object _source;

    public AddStatBonusCommand(Stat stat, object source, float bonus)
    {
        _stat = stat; _source = source; _bonusAmount = bonus;
    }
    public void Execute()
    {
        _stat.AddBonus(_source, _bonusAmount);
    }
}