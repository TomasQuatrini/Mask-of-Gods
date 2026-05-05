using System.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Equippable/Weapon Item")]
public class WeaponItemData : EquippableItemData
{
    [Header("Weapon Type")]
    public WeaponAttackType weaponAttackType;

    [Header("Common Combat")]
    public float staminaCost = 10f;
    public float cooldown = 0.5f;

    [Header("Melee")]
    public MeleeAttackData meleeAttackData;

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 12f;
}