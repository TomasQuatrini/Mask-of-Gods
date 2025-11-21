using UnityEngine;

public class UniversalSpawn : MonoBehaviour
{
    public static UniversalSpawn Instance { get; private set; }

    [SerializeField] private Transform _spawnStaminaPosition;
    [SerializeField] private Transform _spawnHealthPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }

    public Vector3 StaminaSpawn()
    {
        return _spawnStaminaPosition.position;
    }
    public Vector3 HealthSpawn()
    {
        return _spawnHealthPosition.position;
    }
}
