using System.Inventory;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerStaminaSM _playerStaminaSM;
    private PlayerInventoryComponent _inventoryComponent;
    [SerializeField] private HUDInventoryController _hudInventoryControllerEquippable;
    [SerializeField] private HUDInventoryController _hudInventoryControllerConsumable;

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
        if (HUD_ResourcesBarController.Instance == null)
        {
            Debug.LogWarning("HUDController instance not found. UI binding skipped.", this);
            return;
        }
        if (_hudInventoryControllerEquippable && _hudInventoryControllerConsumable == null)
        {
            Debug.LogWarning("HUDInventoryController not found. Inventory UI binding skipped.", this);
        }
        else
        {
            if (_inventoryComponent == null)
            {
                Debug.LogWarning("PlayerInventoryComponent is null. Inventory UI binding skipped.", this);
            }
            else
            {
                _hudInventoryControllerEquippable.Bind(_inventoryComponent);
                _hudInventoryControllerConsumable.Bind(_inventoryComponent);
            }
        }
        if (_playerHealth == null && _playerStaminaSM == null) 
        {
            Debug.LogWarning("PlayerHealth and PlayerStaminaSM are both null. UI binding skipped.", this);
            return;
        }
        HUD_ResourcesBarController.Instance.BindPlayer(_playerHealth, _playerStaminaSM);
        _hudInventoryControllerConsumable.Bind(_inventoryComponent);
        _hudInventoryControllerEquippable.Bind(_inventoryComponent);
    }
}
