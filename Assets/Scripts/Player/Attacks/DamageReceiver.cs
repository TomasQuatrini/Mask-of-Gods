using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float damageOnContact = 10f;
    [SerializeField] private string damageTag = "Damage";
    [SerializeField] private LayerMask damageLayer;

    private IHealth _health;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        if (_health == null)
            Debug.LogWarning($"{name} no tiene HealthSystem asignado.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si no tiene sistema de salud, no hacemos nada
        if (_health == null) return;

        // Verificamos por tag o layer
        bool hasDamageTag = other.CompareTag(damageTag);
        bool hasDamageLayer = (damageLayer.value & (1 << other.gameObject.layer)) != 0;

        if (hasDamageTag || hasDamageLayer)
        {
            // Obtenemos el daño del objeto, si tiene una fuente de daño
            float damageAmount = damageOnContact;
            var damageSource = other.GetComponent<DamageSource>();
            if (damageSource != null)
                damageAmount = damageSource.GetDamage();

            _health.TakeDamage(damageAmount);
            Debug.Log($"{name} recibió {damageAmount} de daño de {other.name}");
        }
    }
}
