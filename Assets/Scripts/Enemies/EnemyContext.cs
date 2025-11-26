using UnityEngine;
using UnityEngine.AI;

public class EnemyContext : MonoBehaviour
{
    [Header("Componentes")]
    public NavMeshAgent Agent { get; private set; }
    public Transform Transform { get; private set; }
    public IEnemyAttack ComponentAttack { get; private set; }

    [SerializeField] private Transform _target;   // Asignar en inspector o por código
    public Transform Target => _target;

    public EnemyData EnemyData;

    [Header("Patrulla")]
    [SerializeField] private Transform[] _patrolPoints;
    public Transform[] PatrolPoints => _patrolPoints;   
    public float PatrolWaitTime => EnemyData.patrolWaitTime; 

    [Header("Detección / Ataque")]
      
    public float DetectionRadius => EnemyData.detectionRadius; 
    public float AttackRange => EnemyData.attackRange; 

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        ComponentAttack = GetComponentInChildren<IEnemyAttack>();
        Transform = transform;

        if (Agent == null)
            Debug.LogError("[EnemyContext] Falta NavMeshAgent", this);
        if (_target == null)
            Debug.LogWarning("[EnemyContext] target no asignado", this);
        if (_patrolPoints == null || _patrolPoints.Length == 0)
            Debug.LogWarning("[EnemyContext] No hay puntos de patrulla asignados", this);
    }

    public float DistanceToTarget()
    {
        if (_target == null) return Mathf.Infinity;
        return Vector3.Distance(Transform.position, _target.position);
    }

    public bool IsTargetWithinDetection()
    {
        return DistanceToTarget() <= DetectionRadius;
    }

    public bool IsTargetWithinAttackRange()
    {
        return DistanceToTarget() <= AttackRange;
    }
}