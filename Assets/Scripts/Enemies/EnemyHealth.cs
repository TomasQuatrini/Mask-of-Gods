using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyContext _context;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;
    public bool IsDead => _currentHealth <= 0f;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action OnDeath;
    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
      
    public bool Heal(float amount)
    {
        if (_currentHealth == _maxHealth) return false;
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
        return true;
    }

   
    private void Die()
    {
        OnDeath?.Invoke();
    }
    
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth);
    }
}

