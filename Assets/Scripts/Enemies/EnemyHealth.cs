using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [Header("Configuración de Vida")]
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    [Header("Eventos")]
    public UnityEvent onDeath;
    public UnityEvent<float> onHealthChanged; 
    private bool _isDead = false;
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        if (_isDead) return;

        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        Debug.Log($"{name} recibió {amount} de daño. Vida restante: {_currentHealth}");

        onHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            Die();
    }

  
    public bool Heal(float amount)
    {
        if (_isDead || _currentHealth == _maxHealth) return false;

        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);       
        onHealthChanged?.Invoke(_currentHealth);
        return true;
    }

   
    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        Debug.Log($"{name} ha muerto.");
        onDeath?.Invoke();
       
        Destroy(gameObject);
    }
    
    public void ResetHealth()
    {
        _isDead = false;
        _currentHealth = _maxHealth;
        onHealthChanged?.Invoke(_currentHealth);
    }

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsDead => _isDead;

}

