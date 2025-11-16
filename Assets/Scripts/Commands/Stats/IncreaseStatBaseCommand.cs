using Game.Stats;
public class IncreaseStatBaseCommand : ICommand
{
    private readonly Stat _stat;
    private readonly float _increaseAmount;
    public IncreaseStatBaseCommand(Stat stat, float increaseAmount)
    {
        _stat = stat; 
        _increaseAmount = increaseAmount;
    }
    public void Execute()
    {
        _stat.AddValue(_increaseAmount);
    }
}