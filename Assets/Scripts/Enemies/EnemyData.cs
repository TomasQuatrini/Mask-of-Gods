using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Game/Enemies/Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public float patrolWaitTime = 2f;
    public float detectionRadius = 8f;
    public float attackRange = 2f;
}
