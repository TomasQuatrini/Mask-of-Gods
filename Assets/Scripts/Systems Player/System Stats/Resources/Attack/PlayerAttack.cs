using UnityEngine;
using System.Inventory;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerContext _context;
    private PlayerInventoryComponent _inventory;
    private InputPlayer _input;
    private PlayerStaminaSM _stamina;

    private WeaponItemData _equippedWeapon;
    [SerializeField] private GameObject _meleeHitboxObj;
    [SerializeField] private MeleeHitbox _meleeHitbox;
    private float _nextAttackTime;
    private bool _isAttacking;

    private void Awake()
    {
        _context = GetComponentInParent<PlayerContext>();
        _inventory = _context.Inventory;
        _input = InputPlayer.Instance;
        _stamina = _context.Stamina;
    }

    private void Start()
    {
        _inventory.OnWeaponEquipped += OnWeaponEquipped;
        _input.OnAttack += TryAttack;
        if (_meleeHitboxObj != null)
        {
            _meleeHitboxObj.SetActive(false);
        }
    }

    private void TryAttack()
    {
        if (_equippedWeapon == null)
        {
            Debug.Log("No weapon equipped.");
            return;
        }
        if (_isAttacking) return;
        if (Time.time < _nextAttackTime) return;
        if (_context.Defense.isDefending || _context.Defense == null) return;
        if (_stamina == null || _stamina.StaminaResource.Current < _equippedWeapon.staminaCost) return;
        switch (_equippedWeapon.weaponAttackType)
        {
            case WeaponAttackType.Melee:
                StartCoroutine(ExecuteMeleeAttack());
                break;
            //agregar casos para otros tipos de ataque como proyectiles, magias, etc.
            default:
                Debug.LogWarning("Unknown attack type.");
                break;
        }
    }

    private void OnWeaponEquipped(WeaponItemData weapon)
    {
        _equippedWeapon = weapon;
    }

    private void OnDestroy()
    {
        _inventory.OnWeaponEquipped -= OnWeaponEquipped;
        _input.OnAttack -= TryAttack;
    }

    private IEnumerator ExecuteMeleeAttack()
    {
        if (_equippedWeapon == null) yield break;
        if (_equippedWeapon.meleeAttackData == null) yield break;
        if (_meleeHitbox == null || _meleeHitboxObj == null) yield break;

        _isAttacking = true;
        _stamina.StaminaResource.Spend(_equippedWeapon.staminaCost);
        _meleeHitbox.SetAttackData(_equippedWeapon.meleeAttackData);
        _meleeHitboxObj.SetActive(true);
        yield return new WaitForSeconds(_equippedWeapon.meleeAttackData.activeTime);
        _meleeHitboxObj.SetActive(false);
        _isAttacking = false;
    }
}
