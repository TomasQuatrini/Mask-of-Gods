using Game.Stats;

public class RemoveStatBonusCommand : ICommand
{
    private readonly Stat _stat;
    private readonly object _source;
    private readonly float _bonusAmount;

    public RemoveStatBonusCommand(Stat stat, object source, float bonus)
    {
        _stat = stat; 
        _source = source; 
        _bonusAmount = bonus;
    }

    public void Execute()
    {
        _stat.RemoveBonusBySource(_source);
    }
}