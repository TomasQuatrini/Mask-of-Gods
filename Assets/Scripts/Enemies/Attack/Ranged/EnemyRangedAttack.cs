using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _attackCooldown = 1.5f;

    private float _nextAttackTime;
    private EnemyContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<EnemyContext>();
        if (_ctx == null)
            Debug.LogError("[EnemyRangedAttack] Falta EnemyContext", this);

        if (_shootOrigin == null)
            Debug.LogError("[EnemyRangedAttack] Falta ShootOrigin", this);

        if (_projectilePrefab == null)
            Debug.LogError("[EnemyRangedAttack] Falta ProjectilePrefab", this);
    }

    public void Attack()
    {
        if (Time.time < _nextAttackTime)
            return;

        if (_ctx.Target == null || _shootOrigin == null || _projectilePrefab == null)
            return;

        _nextAttackTime = Time.time + _attackCooldown;

        // Apuntar hacia el player
        Vector3 dir = (_ctx.Target.position - _shootOrigin.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);

        Instantiate(_projectilePrefab, _shootOrigin.position, rot);
    }
}

