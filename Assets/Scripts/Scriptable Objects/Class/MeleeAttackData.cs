using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackData", menuName = "ScriptableObjects/Game/Attacks/MeleeData", order = 0)]
public class MeleeAttackData : ScriptableObject
{
    public float damage = 10f;
    public float attackCooldown = 1f;
    public float activeTime = 0.2f;
    public float knockbackForce = 1f;
    public float knockbackDuration = 1f;
}
