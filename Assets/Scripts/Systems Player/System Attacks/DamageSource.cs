using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float damage = 15f;

    public float GetDamage() => damage;
}
