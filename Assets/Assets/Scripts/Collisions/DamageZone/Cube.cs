using UnityEditor.UIElements;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private string tag = "Player";
    [SerializeField] private float damage = 10;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            var health = other.GetComponent<IHealth>();
            if (health == null) return;
            health.TakeDamage(damage);
            Debug.Log($"{other} ha sido dañado con {damage}");
            Debug.Log($"Health es {health.CurrentHealth}");
        }
    }
}
