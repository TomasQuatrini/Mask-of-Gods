using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private Collider Collider;
    [SerializeField] private MeleeAttackData attackData;
    private CombatTarget owner;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        CombatTarget target = GetComponentInParent<CombatTarget>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeInHierarchy)
            return;      
        CombatTarget target = other.GetComponentInParent<CombatTarget>();
        if (target == null || owner == null)
        {
            return;
        }
        if (target.Team == owner.Team)
        {
            return;
        }
        var defense = other.GetComponentInParent<IDefense>();
        if (defense != null)
        {
            if (defense.isDefending == true)
            {
                return;
            }
        }
        var health = other.GetComponentInParent<IHealth>();
        if (health != null)
        {
            health.TakeDamage(attackData.damage);
        }
        var knock = other.GetComponentInParent<IKnockbackeable>();
        if (knock != null)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            dir.y = 0f;
            float force = attackData.knockbackForce;
            float duration = attackData.knockbackDuration;
            knock.ApplyKnockback(dir * force, duration);
        }
        else
        {
            Debug.Log("no se puede aplicar el empuje");
        }
    }
}

public enum Team
{
    Player,
    Enemy
}
