using System.Inventory;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerStaminaSM _playerStaminaSM;
    private PlayerInventoryComponent _inventoryComponent;
    
    private PlayerContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        {
            if (_ctx == null)
            {
                Debug.LogError("PlayerUIBinder no encontró PlayerContext en los padres!", this);
                enabled = false;
                return;
            }
        }
        _playerHealth = _ctx.Health;
        _playerStaminaSM = _ctx.Stamina;
        _inventoryComponent = _ctx.Inventory;
    }

    private void Start()
    {
        if (HUDResourcesBarController.Instance == null)
        {
            Debug.LogWarning("HUDController instance not found. UI binding skipped.", this);
            return;
        }
        if (HUDInventoryController.Instance == null)
        {
            Debug.LogWarning("HUDInventoryController instance not found. Inventory UI binding skipped.", this);
        }
        else
        {
            if (_inventoryComponent == null)
            {
                Debug.LogWarning("PlayerInventoryComponent is null. Inventory UI binding skipped.", this);
            }
            else
            {
                HUDInventoryController.Instance.Bind(_inventoryComponent);
            }
        }
        if (_playerHealth == null && _playerStaminaSM == null) 
        {
            Debug.LogWarning("PlayerHealth and PlayerStaminaSM are both null. UI binding skipped.", this);
            return;
        }
        HUDResourcesBarController.Instance.BindPlayer(_playerHealth, _playerStaminaSM);
        HUDInventoryController.Instance.Bind(_inventoryComponent);
    }
}
