using UnityEngine;

public class CombatTarget : MonoBehaviour
{
    [SerializeField] private Team team;
    public Team Team => team;
}
