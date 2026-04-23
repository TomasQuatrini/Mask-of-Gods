using UnityEngine;
using Fusion;
using System.Inventory;

public class NetPlayerUIBinder : NetworkBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerStaminaSM _playerStaminaSM;
    private PlayerInventoryComponent _inventoryComponent;
    private PlayerContext _ctx;
    [SerializeField] private HUDInventoryController _hudInventoryConsumable;

    public override void Spawned()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        if (!Object.HasInputAuthority) return;
        _playerHealth = _ctx.Health;
        _playerStaminaSM = _ctx.Stamina;
        _inventoryComponent = _ctx.Inventory;
        if (HUDResourcesBarController.Instance == null)
        {
            Debug.LogWarning("HUDController instance not found. UI binding skipped.", this);
            return;
        }
        if (_playerHealth == null && _playerStaminaSM == null) return;
        _hudInventoryConsumable.Bind(_inventoryComponent);
        HUDResourcesBarController.Instance.BindPlayer(_playerHealth, _playerStaminaSM);
    }    
}