using UnityEngine;
using Fusion;
using System.Inventory;

public class NetItemConsume : NetworkBehaviour
{
    private PlayerContext _ctx;
    private PlayerInventoryComponent _inventory;
    private PlayerHealth _health;
    private PlayerStaminaSM _stamina;

    [SerializeField] private ItemDataBase _itemDataBase;
    [SerializeField] private ItemData _potionStamina;
    [SerializeField] private ItemData _potionHealth;

    public override void Spawned()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        if (_ctx == null)
        {
            Debug.LogError("[NetItemConsume] No encontré PlayerContext");
            return;
        }

        _inventory = _ctx.Inventory;
        _health = _ctx.Health;
        _stamina = _ctx.Stamina;

        if (_inventory == null)
            Debug.LogError("[NetItemConsume] Inventory es null en Spawned");
        if (_itemDataBase == null)
            Debug.LogError("[NetItemConsume] ItemDataBase no asignado en el inspector");
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasInputAuthority) return;
        if (!GetInput<PlayerNetworkInput>(out var inputPlayer)) return;

        if (inputPlayer.ConsumeHealth)
        {
            if (_potionHealth == null)
            {
                Debug.LogWarning("[NetItemConsume] _potionHealth no asignado");
            }
            else
            {
                Rpc_RequestConsume(_potionHealth.Id);
            }
        }

        if (inputPlayer.ConsumeStamina)
        {
            if (_potionStamina == null)
            {
                Debug.LogWarning("[NetItemConsume] _potionStamina no asignado");
            }
            else
            {
                Rpc_RequestConsume(_potionStamina.Id);
            }
        }
    }

    
    public void TryConsumeHealthPotion(ItemData healthPotion)
    {
        if (!HasInputAuthority) return;
        if (healthPotion == null) return;

        Rpc_RequestConsume(healthPotion.Id);
    }

    public void TryConsumeStaminaPotion(ItemData staminaPotion)
    {
        if (!HasInputAuthority) return;
        if (staminaPotion == null) return;

        Rpc_RequestConsume(staminaPotion.Id);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestConsume(string itemId, RpcInfo info = default)
    {
        if (!HasStateAuthority) return;
        if (string.IsNullOrEmpty(itemId)) return;

        EnsureRefs();
        if (_inventory == null)
        {
            Debug.LogWarning("[NetItemConsume] Inventory null en server");
            return;
        }
        if (_itemDataBase == null)
        {
            Debug.LogWarning("[NetItemConsume] ItemDataBase null en server");
            return;
        }

        var itemData = _itemDataBase.GetItemById(itemId);
        if (itemData == null)
        {
            Debug.LogWarning($"[NetItemConsume] No encontré ItemData con id {itemId}");
            return;
        }

        bool consumed = _inventory.ConsumeItem(itemData);
        if (!consumed)
        {
            Debug.Log($"[NetItemConsume] No había {itemData.Name} para consumir");
            return;
        }

        ApplyItemEffect(itemData);
        Debug.Log($"[NetItemConsume] Consumido {itemData.Name}");
    }

    private void EnsureRefs()
    {
        if (_ctx == null)
            _ctx = GetComponentInParent<PlayerContext>();

        if (_ctx != null)
        {
            if (_inventory == null) _inventory = _ctx.Inventory;
            if (_health == null) _health = _ctx.Health;
            if (_stamina == null) _stamina = _ctx.Stamina;
        }
    }

    private void ApplyItemEffect(ItemData itemData)
    {
        if (!(itemData is ConsumableItemData consumable))
        {
            Debug.LogWarning("[NetItemConsume] ItemData no es ConsumableItemData");
            return;
        }

        switch (consumable.Type_Consumable)
        {
            case ConsumableType.Health:
                if (_health != null)
                    _health.Heal(consumable.Amount);
                break;

            case ConsumableType.Stamina:
                if (_stamina != null)
                    _stamina.StaminaResource.Increase(consumable.Amount);
                break;
        }
    }
}