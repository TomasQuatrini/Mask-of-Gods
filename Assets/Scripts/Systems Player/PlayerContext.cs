using UnityEngine;
using System.Inventory;

public class PlayerContext : MonoBehaviour
{
    [Header("Root")]
    public Rigidbody Body { get; private set; }
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
        Body = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();                      
        Movement = GetComponentInChildren<IMovement>();
        CollisionController = GetComponentInChildren<PlayerCollisionController>();
        Health = GetComponentInChildren<PlayerHealth>();
        Stamina = GetComponentInChildren<PlayerStaminaSM>();
        StatsComponent = GetComponentInChildren<PlayerStatsComponent>();
        Inventory = GetComponentInChildren<PlayerInventoryComponent>();
    }
}
