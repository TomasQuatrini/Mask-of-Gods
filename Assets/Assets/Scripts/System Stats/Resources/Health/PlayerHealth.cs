using Game.Stats;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [Header("UI (opcional)")]
    [SerializeField] private Slider _healthSlider;
    public Stat HealthStat { get; private set; }
    public PlayerStats Stats { get; private set; }
    public Resource HealthResource { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        var core = GetComponent<PlayerStatsComponent>();
        Stats = core.Stats;
        HealthStat = Stats.Get(StatType.Health);
        HealthResource = new Resource("Health", 100f, 1f);
        HealthResource.RecomputeMaxFromStat(HealthStat);
    }

    private void Update()
    {
        CurrentHealth = HealthResource.Current;
    }

    private void OnEnable()
    {
        HealthStat.OnValueChanged += OnHealthStatChanged;
        HealthResource.OnMaxChanged += OnHealthMaxChanged;
        HealthResource.OnCurrentChanged += OnHealthCurrentChanged;
        if (_healthSlider)
        {
            _healthSlider.minValue = 0f;
            _healthSlider.maxValue = HealthResource.Max;
            _healthSlider.value = HealthResource.Current;
        }
    }
    

    private void OnHealthStatChanged(float newValue)
    {
        HealthResource.RecomputeMaxFromStat(HealthStat);
    }

    private void OnHealthMaxChanged(float newMax)
    {
        if (_healthSlider)
            _healthSlider.maxValue = newMax;
    }
    
    private void OnHealthCurrentChanged(float newCurrent)
    {
        if (_healthSlider)
            _healthSlider.value = newCurrent;
    }
    private void OnDisable()
    {
        HealthStat.OnValueChanged -= OnHealthStatChanged;
        HealthResource.OnMaxChanged -= OnHealthMaxChanged;
        HealthResource.OnCurrentChanged -= OnHealthCurrentChanged;
    }
    public void Heal(float amount)
    {
        if (HealthResource == null || amount <= 0 ) return;
        HealthResource.Increase(amount);
    }

    public void TakeDamage(float damage)
    {
        if (HealthResource == null || damage <= 0) return;
        HealthResource.Decrease(damage);
    }

    public void Die()
    {
        Debug.Log("Player Died");
        // death animation, sound, etc.
        // restart to checkpoint
        HealthResource.SetToMax();
    }

    
}
