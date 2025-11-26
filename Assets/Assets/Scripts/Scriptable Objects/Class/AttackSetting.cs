using UnityEngine;

[CreateAssetMenu(fileName = "AttackSetting", menuName = "Scriptable Objects/AttackSetting")]
public class AttackSetting : ScriptableObject
{
    public float attackSpeed;
    public float attackCooldown;
    public float attackDuration;
}
