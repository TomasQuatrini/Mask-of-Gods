public interface IHealth
{
    public void TakeDamage(float damage);
    public bool Heal(float amount);
    public event System.Action<float> OnHealthChanged;
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public bool IsDead { get; }
}