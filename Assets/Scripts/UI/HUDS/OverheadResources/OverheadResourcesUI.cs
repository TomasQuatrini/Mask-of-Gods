using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class OverheadResourcesUI : NetworkBehaviour
{
    [SerializeField] private NetPlayerResources _resources;
    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _staminaFill;

    public override void Spawned()
    {
        if (_resources == null)
            _resources = GetComponentInParent<NetPlayerResources>();

        if (_resources == null)
        {
            Debug.LogError("[OverheadVitalsUI] No encontré NetPlayerResources en el padre");
            enabled = false;
            return;
        }

        //Si este player es el local, ocultamos las barritas chiquitas
        if (Object != null && Object.HasInputAuthority)
        {
            gameObject.SetActive(false);
            return;
        }        
        _resources.OnResourcesChanged += HandleResourcesChanged;
        
        HandleResourcesChanged(_resources.NetHealth, _resources.NetStamina);
    }

    private void OnDestroy()
    {
        if (_resources != null)
            _resources.OnResourcesChanged -= HandleResourcesChanged;
    }

    private void LateUpdate()
    {
        if (!gameObject.activeInHierarchy) return;
        var cam = Camera.main;
        if (cam == null) return;
        Vector3 dir = transform.position - cam.transform.position;
        if (dir.sqrMagnitude > 0.0001f)
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void HandleResourcesChanged(float health, float stamina)
    {
        if (_healthFill != null)
            _healthFill.fillAmount = health / _resources.MaxHealth;

        if (_staminaFill != null)
            _staminaFill.fillAmount = stamina / _resources.MaxStamina;
    }
}