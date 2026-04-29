using UnityEngine;
using UnityEngine.UI;

public class UIBarOtherObjectHealth : MonoBehaviour
{
    private IHealth _health;
    [SerializeField] private Image _healthFill;
    private void Start()
    {
        if (_health == null)
        {
            _health = GetComponentInParent<IHealth>();
            if (_health == null )
            {
                _health = GetComponentInParent<IContext>().Health;
            }
        }
        if (_health == null)
        {
            Debug.LogError("Health component not found in parent");
        }
        _health.OnHealthChanged += HandleHealthChanged;

        HandleHealthChanged(_health.CurrentHealth);
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.OnHealthChanged -= HandleHealthChanged;
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

    private void HandleHealthChanged(float health)
    {
        if (_healthFill != null)
            _healthFill.fillAmount = health / _health.MaxHealth;
    }
}