public interface IHealth
{
    public void TakeDamage(float damage);
    public void Heal(float amount);
    public void Die();
    public float CurrentHealth { get; }

}