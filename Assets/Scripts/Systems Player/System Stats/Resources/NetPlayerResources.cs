using UnityEngine;
using Fusion;

public class NetPlayerResources : NetworkBehaviour
{
    private PlayerContext _ctx;
    private PlayerHealth _healthResource;
    private PlayerStaminaSM _staminaResource;

    [Header("Máximos (solo para UI)")]
    private float _maxHealth = 100f;
    private float _maxStamina = 100f;

    [Networked] public float NetHealth { get; private set; }
    [Networked] public float NetStamina { get; private set; }

    public System.Action<float, float> OnResourcesChanged;
    public float MaxHealth => _maxHealth;
    public float MaxStamina => _maxStamina;

    public override void Spawned()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        if (_ctx == null) return;

        _healthResource = _ctx.Health;
        _staminaResource = _ctx.Stamina;

        if (_healthResource == null || _staminaResource == null)
        {
            Debug.LogWarning("[NetPlayerResources] Resources null");
        }

        _maxHealth = _healthResource.MaxHealth;
        _maxStamina = _staminaResource.StaminaResource.Max;

        
        if (HasInputAuthority)
        {
            Rpc_UpdateVitals(
                _healthResource.CurrentHealth,
                _staminaResource.StaminaResource.Current
            );
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (_healthResource == null || _staminaResource == null) return;

        
        if (HasInputAuthority && GetInput<PlayerNetworkInput>(out var inputPlayer))
        {           
            if (inputPlayer.TakeDamaged)
            {
                Rpc_RequestDamage(30);
            }

            float h = _healthResource.CurrentHealth;
            float s = _staminaResource.StaminaResource.Current;
            Rpc_UpdateVitals(h, s);
        }
    }

    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_UpdateVitals(float health, float stamina, RpcInfo info = default)
    {
        NetHealth = health;
        NetStamina = stamina;
        OnResourcesChanged?.Invoke(NetHealth, NetStamina);
    }

    public override void Render()
    {        
        OnResourcesChanged?.Invoke(NetHealth, NetStamina);
    }

    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_RequestDamage(float damage, RpcInfo info = default)
    {
        if (!HasInputAuthority) return;

        if (_healthResource == null)
        {
            _ctx ??= GetComponentInParent<PlayerContext>();
            _healthResource = _ctx?.Health;
            if (_healthResource == null)
            {
                Debug.LogWarning("[NetPlayerResources] HealthResource null en server");
                return;
            }
        }

        _healthResource.TakeDamage(damage);

        NetHealth = _healthResource.CurrentHealth;
        NetStamina = _staminaResource != null
            ? _staminaResource.StaminaResource.Current
            : NetStamina;

        OnResourcesChanged?.Invoke(NetHealth, NetStamina);
    }
}