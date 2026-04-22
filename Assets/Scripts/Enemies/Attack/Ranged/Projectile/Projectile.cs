using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackDuration = 0.5f;

    private float _timer;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        var defense = other.GetComponentInParent<IDefense>();
        if (defense != null)
        {
            if (defense.isDefending == true)
            {
                Destroy(gameObject);
                return;
            }
        }
        var health = other.GetComponentInParent<IHealth>();
        if (health != null)
        {
            health.TakeDamage(_damage);
        }
        var knockback = other.GetComponentInParent<IKnockbackeable>();
        {
            var dir = (other.transform.position - transform.position).normalized;
            knockback?.ApplyKnockback(dir * _knockbackForce, _knockbackDuration);
        }

        Destroy(gameObject);
    }
}