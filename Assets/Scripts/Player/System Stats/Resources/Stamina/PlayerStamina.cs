using UnityEngine;
using UnityEngine.UI;
using Game.Stats;
using System;

public class PlayerStaminaSM : MonoBehaviour
{
    private PlayerContext _ctx;

    [Header("Config (por segundo)")]
    [SerializeField] private StaminaSettings _settings;

    [Header("Input")]
    private InputPlayer _input;

    [Header("Stamina States")]
    private IStaminaState _state;
    private StaminaIdleState _idle;
    private StaminaRunningState _running;
    private StaminaExhaustedState _exhausted;

    private PlayerStats Stats;
    private Stat StaminaStat;
    public Resource StaminaResource { get; private set; }
    public bool TryingToRun { get; private set; }
    public event Action <bool> HasStamina; 

    private void Awake()
    {        
        _ctx = GetComponentInParent<PlayerContext>();
        _input = _ctx.Input;

        var core = _ctx.StatsComponent;
        Stats = core.Stats;        
        StaminaStat = Stats.Get(StatType.Stamina);
        StaminaResource = new Resource("Stamina", _settings.baseMax, 0.25f);
        StaminaResource.RecomputeMaxFromStat(StaminaStat);
       
        _idle = new StaminaIdleState(this);
        _running = new StaminaRunningState(this);
        _exhausted = new StaminaExhaustedState(this);

        ChangeState(_idle);
        Debug.Log($"[{name}] Stats es {Stats}, hash {Stats.GetHashCode()}", this);
    }
    private void Update()
    {
        _state.Tick();
        CheckStamina();
    }

    private void OnEnable()
    {
        StaminaStat.OnValueChanged += OnStaminaStatChanged;    
        _input.OnRun += OnTryRun;
    }

    private void OnStaminaStatChanged(float newValue) => StaminaResource.RecomputeMaxFromStat(StaminaStat);
    
    private void CheckStamina()
    {
        if (StaminaResource.Current > _settings.spendRunning)
        {
            HasStamina?.Invoke(true);
        }
        else
        {
            HasStamina?.Invoke(false);            
        }
    }

    private void OnTryRun(bool pressed)
    { 
        TryingToRun = pressed;
    }
    private void OnDisable()
    {
        StaminaStat.OnValueChanged -= OnStaminaStatChanged;
        _input.OnRun -= OnTryRun;
    }

    public void ChangeState(IStaminaState next)
    {
        _state?.Exit();
        _state = next;
        _state.Enter();
    }
    
    public float SpendRateRunning => _settings.spendRunning;
    public float RegenRateIdle => _settings.regenIdle;
    public float RegenRateExhausted => _settings.regenExhausted;
    
    public bool RecoveredFromExhausted()
    {
        if (StaminaResource.Max <= 0f) return false;
        float percent = (StaminaResource.Current * 100f) / StaminaResource.Max;
        return percent >= _settings.exhaustedRecoverPercent;
    }

    public IStaminaState IdleState => _idle;
    public IStaminaState RunningState => _running;
    public IStaminaState ExhaustedState => _exhausted;    
}