using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class AITarget : MonoBehaviour
{
    public Transform _target;
    public float _AttackDistance;
    public BoxCollider _hitbox;

    private NavMeshAgent _agent;
    private float _distance;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_AttackDistance = _hitbox.size
    }
    void Update()
    {
        _distance = Vector3.Distance(_agent.transform.position, _target.position);
        if (_distance < _AttackDistance)
        {
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
            _agent.destination = _target.position;
        }
    }
}
