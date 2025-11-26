using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _damage = 10f;

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

        var health = other.GetComponentInParent<IHealth>(); // cambia al nombre correcto
        if (health != null)
        {
            health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}