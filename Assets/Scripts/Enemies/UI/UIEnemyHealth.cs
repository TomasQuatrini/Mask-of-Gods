
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private Image _healthFill;
    public void Start()
    {
        if (_enemyHealth == null)
            _enemyHealth = GetComponentInParent<EnemyHealth>();

        if (_enemyHealth == null)
        {
            Debug.LogError("Health component not found in parent");
        }
        _enemyHealth.OnHealthChanged += HandleHealthChanged;

        HandleHealthChanged(_enemyHealth.CurrentHealth);
    }

    private void OnDestroy()
    {
        if (_enemyHealth != null)
            _enemyHealth.OnHealthChanged -= HandleHealthChanged;
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
            _healthFill.fillAmount = health / _enemyHealth.MaxHealth;

    }
}