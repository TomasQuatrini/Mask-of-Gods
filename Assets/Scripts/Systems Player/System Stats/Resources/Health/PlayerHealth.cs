using Game.Stats;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{    
    private PlayerStats Stats;
    private Stat HealthStat;
    private Resource _healthResource;
    private PlayerContext _ctx;

    [Header("Health Info")]
    public Resource HealthResource => _healthResource;
    public float CurrentHealth => _healthResource?.Current ?? 0f;
    public float MaxHealth => _healthResource?.Max ?? 0f;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        var core = _ctx.StatsComponent;
        Stats = core.Stats;
        HealthStat = Stats.Get(StatType.Health);
        _healthResource = new Resource("Health", 100f, 1f);
        _healthResource.RecomputeMaxFromStat(HealthStat);
    }  

    private void OnEnable()
    {
        HealthStat.OnValueChanged += OnHealthStatChanged;              
    }    

    private void OnHealthStatChanged(float newValue)
    {
        _healthResource.RecomputeMaxFromStat(HealthStat);
    } 

    private void OnDisable()
    {
        HealthStat.OnValueChanged -= OnHealthStatChanged;        
    }
    public bool Heal(float amount)
    {
        if (_healthResource == null || amount <= 0 || _healthResource.Current == MaxHealth ) return false;
        _healthResource.Increase(amount);
        return true;
    }

    public void TakeDamage(float damage)
    {
        if (_healthResource == null || damage <= 0) return;
        _healthResource.Decrease(damage);
        if (_healthResource.Current <= 0f) Die();
    }

    public void Die()
    {
        Debug.Log("Player Died");
        // death animation, sound, etc.
        // restart to checkpoint
        _healthResource.SetToMax();
    }
    public bool IsDead
    {
        get { return _healthResource != null && _healthResource.Current <= 0f; }
    }
}
