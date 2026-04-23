using UnityEngine;
using System.Inventory;

public class PlayerWeaponSwitcher : MonoBehaviour
{
    private PlayerContext _context;
    private PlayerInventoryComponent _inventory;
    private InputPlayer _input;

    private void Awake()
    {
        _context = GetComponent<PlayerContext>();
        _inventory = GetComponent<PlayerInventoryComponent>();
        _input = InputPlayer.Instance;
    }
    
    private void OnEnable()
    {
        _input.OnSwitchWeapon += SwitchWeapon;
    }

    private void OnDisable()
    {
        _input.OnSwitchWeapon -= SwitchWeapon;
    }

    private void SwitchWeapon()
    {
        if (_inventory == null) return;
        bool changed = _inventory.EquipNextWeapon();
        if (changed)
        {
            Debug.Log("Arma Equipada");
        }
    }
}
