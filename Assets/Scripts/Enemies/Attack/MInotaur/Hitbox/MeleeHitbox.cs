using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private Collider Collider;
    [SerializeField] private MeleeAttackData _attackData;
    [SerializeField] private Faction _factionTarget;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeInHierarchy)
            return;

        var targetFaction = other.GetComponentInParent<IContext>()?.Faction;
        if (targetFaction != null)
        {
            if (targetFaction != _factionTarget)
            {
                return;
            }
        }
        var defense = other.GetComponentInParent<IDefense>();
        Debug.Log($"MeleeHitbox: Colisionó con {other.name}, Defensa: {defense != null}");
        if (defense != null)
        {
            if (defense.isDefending == true)
            {
                return;
            }
        }
        var health = other.GetComponentInParent<IHealth>();
        Debug.Log($"MeleeHitbox: Colisionó con {other.name}, Salud: {health != null}");
        if (health != null)
        {
            health.TakeDamage(_attackData.damage);
        }
        var knock = other.GetComponentInParent<IKnockbackeable>();
        if (knock != null)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            dir.y = 0f;
            float force = _attackData.knockbackForce;
            float duration = _attackData.knockbackDuration;
            knock.ApplyKnockback(dir * force, duration);
        }
    }

    public void SetAttackData(MeleeAttackData data)
    {
        _attackData = data;
    }
}

public enum Faction
{
    Player,
    Enemy,
    Neutral
}
