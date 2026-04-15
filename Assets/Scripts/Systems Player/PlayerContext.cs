using UnityEngine;
using System.Inventory;

public class PlayerContext : MonoBehaviour, IContext
{
    [Header("Root")]
    public Rigidbody Rigidbody { get; private set; }
    public CapsuleCollider Collider { get; private set; }

    [Header("Logic Components")]   
    public IMovement Movement { get; private set; }
    public PlayerCollisionController CollisionController { get; private set; }
    public PlayerHealth Health { get; private set; }
    public PlayerStaminaSM Stamina { get; private set; }
    public PlayerStatsComponent StatsComponent { get; private set; }
    public PlayerInventoryComponent Inventory { get; private set; }

    [Header("Data Scriptable Objects")]
    [SerializeField] public MovementSettings MovementSettings;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();                      
        Movement = GetComponentInChildren<IMovement>();
        CollisionController = GetComponent<PlayerCollisionController>();
        Health = GetComponentInChildren<PlayerHealth>();
        Stamina = GetComponentInChildren<PlayerStaminaSM>();
        StatsComponent = GetComponentInChildren<PlayerStatsComponent>();
        Inventory = GetComponentInChildren<PlayerInventoryComponent>();
    }

    public void SetPlayerDataForLoad(SaveDataPlayer data)
    {
        transform.position = data.PlayerPosition.Vector3;
        if (data == null) return;
        if (Health != null)
        {
            Health.HealthResource.SetCurrent(data.CurrentPlayerHealth);
        }
        if (Stamina != null)
        {
            Stamina.StaminaResource.SetCurrent(data.CurrentPlayerStamina);
        }
    }
}
