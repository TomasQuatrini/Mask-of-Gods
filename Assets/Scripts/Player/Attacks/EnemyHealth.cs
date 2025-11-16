using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [Header("Configuración de Vida")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Eventos")]
    public UnityEvent onDeath;
    public UnityEvent<float> onHealthChanged; // Pasa el valor actual de vida

    private bool _isDead = false;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Aplica daño al objeto.
    /// </summary>
    public void TakeDamage(float amount)
    {
        if (_isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{name} recibió {amount} de daño. Vida restante: {currentHealth}");

        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// Restaura una cantidad de vida.
    /// </summary>
    public void Heal(float amount)
    {
        if (_isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{name} se curó {amount}. Vida actual: {currentHealth}");
        onHealthChanged?.Invoke(currentHealth);
    }

    /// <summary>
    /// El objeto muere.
    /// </summary>
    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        Debug.Log($"{name} ha muerto.");
        onDeath?.Invoke();

        // Ejemplo: Desactivar el objeto (podés cambiarlo)
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Restaura la vida al máximo.
    /// </summary>
    public void ResetHealth()
    {
        _isDead = false;
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => _isDead;

    void IHealth.Die()
    {
        Die();
    }
}

