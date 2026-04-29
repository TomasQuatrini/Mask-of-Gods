using UnityEngine;
using System;

public class DestructibleHealth : MonoBehaviour, IHealth
{
    private float _currentHealth;
    [SerializeField] private float _maxHealth;
    public event Action <float> OnHealthChanged;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public bool IsDead => throw new System.NotImplementedException();

    private void Start()
    {
            _currentHealth = _maxHealth;
    }

    public bool Heal(float amount)
    {
        if (_currentHealth >= _maxHealth) return false;
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
        return true;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0) return;
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            //animacion destruccion
            Destroy(gameObject);
        }

    }
}
